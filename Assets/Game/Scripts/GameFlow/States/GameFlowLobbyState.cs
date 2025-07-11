using Services.EventService;
using UnityEngine;

namespace Game.GameFlow
{
    public class GameFlowLobbyState : AbstractGameFlowState
    {
        public override GameFlowStateType Type => GameFlowStateType.Lobby;

        public GameFlowLobbyState(GameFlowStateFactory stateFactory,IEventService eventService) : base(stateFactory,eventService)
        {
            
        }

        public override bool CanTransitionTo(GameFlowStateType type)
        {
            return type == GameFlowStateType.Mining;
        }

        public override void StateExit()
        {
            
        }

        public override GameFlowStateType GetBackState()
        {
            return GameFlowStateType.Lobby;
        }
        
        public override void StateEnter()
        {
            _eventService.Raise(new GameFlowStateEvent(GameFlowStateType.Lobby));
        }
    }
}