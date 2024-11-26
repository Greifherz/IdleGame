using Game.Data;
using Services.EventService;
using UnityEngine;

namespace Game.Scripts.GameLogic
{
    public class GameplayLogic
    {
        private IEventService _eventService;
        private IEventHandler _attackEventHandler;
        
        private IEnemyCharacter _enemyCharacter;
        private IPlayerCharacter _playerCharacter;

        public GameplayLogic(IEventService eventService)
        {
            _eventService = eventService;
            _attackEventHandler = new AttackEventHandler(AttackAction);
            _eventService.RegisterListener(_attackEventHandler,EventPipelineType.GameplayPipeline);

            GetCharacters();
        }

        private void GetCharacters()
        {
            _enemyCharacter = new EnemyCharacter("Dummy",1,5,0,0);
            _playerCharacter = new PlayerCharacter(new Character("Player",10,0,1),1,0);
        }

        //For now and FTUE most likely this will be called through an event fired by a button press. Later stages the event will be fired from a Tick/Schedule
        private void AttackAction(IAttackEvent attackEvent)
        {
            Debug.Log($"Attack event - {_playerCharacter.Name} x {_enemyCharacter.Name}");
            
            _enemyCharacter.TakeDamage(_playerCharacter.AttackPoints);
            _playerCharacter.TakeDamage(_enemyCharacter.AttackPoints);
        }
    }
}