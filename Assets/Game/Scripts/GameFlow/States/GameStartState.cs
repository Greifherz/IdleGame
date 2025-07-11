using Services.EventService;
using UnityEngine;

namespace Game.GameFlow
{
    public class GameStartState : AbstractGameFlowState
    {
        public override GameFlowStateType Type => GameFlowStateType.Start;

        public GameStartState (GameFlowStateFactory stateFactory,IEventService eventService) : base(stateFactory,eventService)
        {
            
        }

        public override bool CanTransitionTo(GameFlowStateType type)
        {
            return type == GameFlowStateType.Lobby;
        }

        public override void StateExit()
        {
            
        }

        public override GameFlowStateType GetBackState()
        {
            //Should quit the game?
            return GameFlowStateType.Lobby;
        }

        public override void StateEnter()
        {
            _eventService.Raise(new GameFlowStateEvent(GameFlowStateType.Start));
        }
    }
}