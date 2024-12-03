using System.Collections.Generic;
using Game.Data;
using Game.Data.GameplayData;
using ServiceLocator;
using Services.EventService;
using Services.Scheduler;

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
            
            _attackEventHandler = new AttackEventHandler(AttackAction);
            _eventService.RegisterListener(_attackEventHandler,EventPipelineType.GameplayPipeline);

            GetCharacters();
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

        private void GetCharacters()
        {
            //TODO - Move this to a proper class that will populate this
            var EnemyCount = _gameplayDataService.EnemyCount;
            for (int i = 0; i < EnemyCount; i++)
            {
                var Enemy = _gameplayDataService.GetEnemyData(i);
                var EnemyCharacterObject = Enemy.ToEnemyCharacter(OnEnemyDeath);
                _enemyCharacter.Add(EnemyCharacterObject);
                _eventService.Raise(new IdleItemUpdateViewEvent(i,EnemyCharacterObject.HealthPercentage,EnemyCharacterObject.Name),EventPipelineType.ViewPipeline);
            }
            
            _playerCharacter = new PlayerCharacter("Player",1,0,10,0,1,OnPlayerDeath);
            _eventService.Raise(new PlayerHealthUpdateViewEvent(1,$"{_playerCharacter.CurrentHealthPoints}/{_playerCharacter.HealthPoints}"),EventPipelineType.ViewPipeline);
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
            _enemyCharacter[EnemyIndex].TakeDamage(_playerCharacter.AttackPoints);
            _playerCharacter.TakeDamage(_enemyCharacter[EnemyIndex].AttackPoints);
            
            _eventService.Raise(new IdleItemUpdateViewEvent(EnemyIndex,_enemyCharacter[EnemyIndex].HealthPercentage,_enemyCharacter[EnemyIndex].Name),EventPipelineType.ViewPipeline);
        }
    }
}