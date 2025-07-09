using Services.EventService;
using UnityEngine;

namespace Game.GameFlow
{
    public class GameFlowLobbyState : IGameFlowState
    {
        public GameFlowStateType Type => GameFlowStateType.Lobby;
        private IEventService _eventService;
        
        public GameFlowLobbyState(IEventService eventService)
        {
            _eventService = eventService;
        }


        public bool CanTransitionTo(GameFlowStateType type)
        {
            return type == GameFlowStateType.Mining;
        }

        public IGameFlowState TransitionTo(GameFlowStateType type)
        {
            if (CanTransitionTo(type))
            {
                return new GameFlowMiningState(_eventService);
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
            _eventService.Raise(new GameFlowStateEvent(GameFlowStateType.Lobby));
        }
    }
}