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
        private List<IEnemyCharacter> _enemyCharacter = new List<IEnemyCharacter>(10);//Hardcoded 10 for now
        private IPlayerCharacter _playerCharacter;

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
        }

        private void SetAuto()
        {
            var Handle = _schedulerService.Schedule(1);
            Handle.OnScheduleTick += OnAuto;
        }

        private void OnAuto()
        {
            var Handle = _schedulerService.Schedule(1);
            Handle.OnScheduleTick += OnAuto;
            
            _eventService.Raise(new AttackEvent(0),EventPipelineType.GameplayPipeline);
        }

        private void GetCharacters(GameplayPersistentData data)
        {
            //TODO - Move this to a proper class that will populate this
            var EnemyCount = _gameplayDataService.EnemyCount;
            for (var Index = 0; Index < EnemyCount; Index++)
            {
                var Enemy = _gameplayDataService.GetEnemyData(Index);
                var EnemyCharacterObject = Enemy.ToEnemyCharacter(OnEnemyDeath);
                
                _enemyCharacter.Add(EnemyCharacterObject);
                _eventService.Raise(new IdleItemUpdateViewEvent(Index,EnemyCharacterObject.HealthPercentage,EnemyCharacterObject.KillCount,EnemyCharacterObject.Name),EventPipelineType.ViewPipeline);
            }

            _playerCharacter = new PlayerCharacter(data.PlayerPersistentData,OnPlayerDeath);
            _eventService.Raise(new PlayerHealthUpdateViewEvent(_playerCharacter.HealthPercentage,$"{_playerCharacter.CurrentHealthPoints}/{_playerCharacter.HealthPoints}"),EventPipelineType.ViewPipeline);
        }

        private void OnPlayerDeath(IPlayerCharacter deadPlayer)
        {
            _eventService.Raise(new PlayerDeathEvent(deadPlayer),EventPipelineType.GameplayPipeline);
        }

        private void OnEnemyDeath(IEnemyCharacter deadCharacter)
        {
            _eventService.Raise(new DeathEvent(deadCharacter),EventPipelineType.GameplayPipeline);
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