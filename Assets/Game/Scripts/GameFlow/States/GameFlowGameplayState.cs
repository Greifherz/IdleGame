using Game.Scripts.GameLogic;
using Services.EventService;
using UnityEngine;

namespace Game.GameFlow
{
    public class GameFlowGameplayState : IGameFlowState
    {
        public GameFlowStateType Type => GameFlowStateType.Gameplay;
        private IEventService _eventService;

        private GameplayLogic _gameplayLogic;

        public GameFlowGameplayState(IEventService eventService)
        {
            _eventService = eventService;
        }

        public bool CanTransitionTo(GameFlowStateType type)
        {
            return type == GameFlowStateType.Lobby;
        }

        public IGameFlowState TransitionTo(GameFlowStateType type)
        {
            if (CanTransitionTo(type))
            {
                return new GameFlowLobbyState(_eventService);
            }
            
            Debug.LogError($"Tried to transition from {Type} to {type} and it's not allowed");
            return this;
        }

        public GameFlowStateType GetBackState()
        {
            return GameFlowStateType.Lobby;
        } 
        
        public void StateEnter()
        {
            _eventService.Raise(new GameFlowStateEvent(GameFlowStateType.Gameplay));
            _gameplayLogic = new GameplayLogic(_eventService);
        }
    }
}