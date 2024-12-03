using Game.Data;
using ServiceLocator;
using Services.EventService;
using Services.Scheduler;
using UnityEngine;

namespace Game.Scripts.GameLogic
{
    public class GameplayLogic
    {
        private IEventService _eventService;
        private ISchedulerService _schedulerService;
        private IEventHandler _attackEventHandler;
        private IEventHandler _deathEventHandler;
        
        private IEnemyCharacter _enemyCharacter;
        private IPlayerCharacter _playerCharacter;

        public GameplayLogic(IEventService eventService)
        {
            _eventService = eventService;
            _schedulerService = Locator.Current.Get<ISchedulerService>();
            _attackEventHandler = new AttackEventHandler(AttackAction);
            _eventService.RegisterListener(_attackEventHandler,EventPipelineType.GameplayPipeline);

            GetCharacters();
            
            _eventService.Raise(new IdleItemUpdateViewEvent(0,1,_enemyCharacter.Name),EventPipelineType.ViewPipeline);
            _eventService.Raise(new PlayerHealthUpdateViewEvent(1,$"{_playerCharacter.CurrentHealthPoints}/{_playerCharacter.HealthPoints}"),EventPipelineType.ViewPipeline);

            
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
            _enemyCharacter = new EnemyCharacter(1,"Dummy",1,5,0,0,OnEnemyDeath);
            _playerCharacter = new PlayerCharacter("Player",1,0,10,0,1,OnPlayerDeath);
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
            _enemyCharacter.TakeDamage(_playerCharacter.AttackPoints);
            _playerCharacter.TakeDamage(_enemyCharacter.AttackPoints);
            
            _eventService.Raise(new IdleItemUpdateViewEvent(0,_enemyCharacter.HealthPercentage,_enemyCharacter.Name),EventPipelineType.ViewPipeline);
        }
    }
}