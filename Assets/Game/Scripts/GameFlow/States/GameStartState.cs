using Services.EventService;
using UnityEngine;

namespace Game.GameFlow
{
    public class GameStartState : IGameFlowState
    {
        public GameFlowStateType Type => GameFlowStateType.Start;
        private IEventService _eventService;

        public GameStartState(IEventService eventService)
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

        public void StateEnter()
        {
            _eventService.Raise(new GameFlowStateEvent(GameFlowStateType.Start));
        }
    }
}