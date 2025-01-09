using System.Collections.Generic;
using Game.Data;
using Game.Data.GameplayData;
using Game.Data.PersistentData;
using ServiceLocator;
using Services.EventService;
using Services.GameDataService;
using Services.Scheduler;
using UnityEngine;

namespace Game.GameLogic
{
    public class GameplayLogic
    {
        //Dependencies
        //Services
        private IEventService _eventService;
        private ISchedulerService _schedulerService;

        //Game
        private IGameplayDataService _gameplayDataService;
        private IGamePersistenceDataService _gamePersistenceDataService;

        //Components
        private IEventHandler _attackEventHandler;
        private IEventHandler _deathEventHandler;

        //Properties
        private List<IEnemyCharacter> _enemyCharacter = new List<IEnemyCharacter>(10); //Hardcoded 10 for now
        private IPlayerCharacter _playerCharacter;

        private static readonly int[] ENEMY_AUTO_CHECKPOINTS = new []{10,25,100,500,1000};//TODO - create an auto object and service instead of having this here
        private int[] _enemiesOnAuto = new int[]{};
        private List<int> _enemiesOnAutoAux;

        public GameplayLogic(IEventService eventService)
        {
            _eventService = eventService;
            _schedulerService = Locator.Current.Get<ISchedulerService>();
            _gameplayDataService = Locator.Current.Get<IGameplayDataService>();
            _gamePersistenceDataService = Locator.Current.Get<IGamePersistenceDataService>();
            
            _attackEventHandler = new AttackEventHandler(AttackAction);
            _eventService.RegisterListener(_attackEventHandler,EventPipelineType.GameplayPipeline);

            var PersistentData = _gamePersistenceDataService.LoadPersistentGameplayData();

            GetCharacters(PersistentData);
            SetAuto();
        }

        private void SetAuto()
        {
            if (_enemiesOnAuto.Length <= 0) return;
            var Handle = _schedulerService.Schedule(1);
            Handle.OnScheduleTick += OnAuto;
        }

        private void OnAuto()
        {
            var Handle = _schedulerService.Schedule(1);
            Handle.OnScheduleTick += OnAuto;

            for (var Index = 0; Index < _enemiesOnAuto.Length; Index++)
            {
                _eventService.Raise(new AttackEvent(_enemiesOnAuto[Index]),EventPipelineType.GameplayPipeline);
            }
        }

        private void GetCharacters(GameplayPersistentData data)
        {
            //TODO - Move this to a proper class that will populate this
            var EnemyCount = _gameplayDataService.EnemyCount;

            _enemiesOnAutoAux ??= new List<int>(EnemyCount);
            _enemiesOnAutoAux.Clear();
            
            for (var Index = 0; Index < EnemyCount; Index++)
            {
                var Enemy = _gameplayDataService.GetEnemyData(Index);
                var EnemyCharacterObject = Enemy.ToEnemyCharacter(Index,OnEnemyDeath);
                if (EnemyCharacterObject.KillCount >= 10)
                {
                    _enemiesOnAutoAux.Add(Index);
                }
                
                _enemyCharacter.Add(EnemyCharacterObject);
                _eventService.Raise(new IdleItemUpdateViewEvent(Index,EnemyCharacterObject.HealthPercentage,EnemyCharacterObject.KillCount,EnemyCharacterObject.Name),EventPipelineType.ViewPipeline);
            }

            _playerCharacter = _gameplayDataService.GameplayData.PlayerCharacter;
            //Find a way to hook-up both player death and playerLevelUp to this character in an elegant manner

            _eventService.Raise(new PlayerHealthUpdateViewEvent(_playerCharacter.HealthPercentage,$"{_playerCharacter.CurrentHealthPoints}/{_playerCharacter.HealthPoints}"),EventPipelineType.ViewPipeline);
        }

        private void OnPlayerLevelUp(IPlayerCharacter obj)
        {
            //Wrong way send event to persist, it should be on specific triggers: Game sent to background, Game close, Back to lobby and every 10 secs or so
            _eventService.Raise(new GameplayDataPersistenceEvent(),EventPipelineType.ServicesPipeline);
        }

        private void OnPlayerDeath(IPlayerCharacter deadPlayer)
        {
            _eventService.Raise(new PlayerDeathEvent(deadPlayer),EventPipelineType.GameplayPipeline);
        }

        private void OnEnemyDeath(IEnemyCharacter deadCharacter)
        {
            if (deadCharacter.KillCount >= 10)
            {
                var RunAuto = _enemiesOnAuto.Length == 0;
                if (RunAuto)
                {
                    _enemiesOnAuto = new[] {deadCharacter.Id};
                    SetAuto();    
                }
                else
                {
                    _enemiesOnAutoAux.Clear();
                    var Found = false;
                    for (var Index = 0; Index < _enemiesOnAuto.Length; Index++)
                    {
                        _enemiesOnAutoAux.Add(_enemiesOnAuto[Index]);
                        if (_enemiesOnAuto[Index] != deadCharacter.Id) continue;
                        Found = true;
                        break;
                    }

                    if (!Found)
                    {
                        _enemiesOnAutoAux.Add(deadCharacter.Id);
                        
                        _enemiesOnAuto = new int[_enemiesOnAutoAux.Count];
                        for (var Index = 0; Index < _enemiesOnAutoAux.Count; Index++)
                        {
                            _enemiesOnAuto[Index] = _enemiesOnAutoAux[Index];
                        }
                    }
                }
                
            }

            var deathEvent = new DeathEvent(deadCharacter);
            _eventService.Raise(deathEvent,EventPipelineType.GameplayPipeline);
            _eventService.Raise(deathEvent,EventPipelineType.ViewPipeline);
            
            _playerCharacter.EarnExperience(deadCharacter.XpReward);
            if (_playerCharacter.ExperiencePoints >=
                _gameplayDataService.GetLevelRequirement(_playerCharacter.Level).RequiredXP)
            {
                _playerCharacter.LevelUp();
                //LevelUpEvent
            }
            _eventService.Raise(new PlayerDataUpdatedEvent(_playerCharacter),EventPipelineType.GameplayPipeline);
        }

        //For now and FTUE most likely this will be called through an event fired by a button press. Later stages the event will be fired from a Tick/Schedule
        private void AttackAction(IAttackEvent attackEvent)
        {
            var EnemyIndex = attackEvent.EnemyIndex;
            var AttackedEnemy = _enemyCharacter[EnemyIndex];
            
            var Before = AttackedEnemy.CurrentHealthPoints;
            _enemyCharacter[EnemyIndex].TakeDamage(_playerCharacter.AttackPoints);
            if (Before != AttackedEnemy.CurrentHealthPoints)
            {
                _eventService.Raise(new EnemyDataUpdatedEvent(AttackedEnemy),EventPipelineType.GameplayPipeline);
                _eventService.Raise(new IdleItemUpdateViewEvent(EnemyIndex,AttackedEnemy.HealthPercentage,AttackedEnemy.KillCount,AttackedEnemy.Name),EventPipelineType.ViewPipeline);
            }

            Before = _playerCharacter.CurrentHealthPoints;
            _playerCharacter.TakeDamage(_enemyCharacter[EnemyIndex].AttackPoints);

            if (Before != _playerCharacter.CurrentHealthPoints)
            {
                _eventService.Raise(new PlayerDataUpdatedEvent(_playerCharacter),EventPipelineType.GameplayPipeline);
                _eventService.Raise(new PlayerHealthUpdateViewEvent(_playerCharacter.HealthPercentage,$"{_playerCharacter.CurrentHealthPoints}/{_playerCharacter.HealthPoints}"),EventPipelineType.ViewPipeline);
            }
        }
    }
}