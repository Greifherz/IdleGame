using UnityEngine;

namespace Game.GameFlow
{
    public class GameStartState : IGameFlowState
    {
        public GameFlowStateType Type => GameFlowStateType.Start;

        public bool CanTransitionTo(GameFlowStateType type)
        {
            return type == GameFlowStateType.Lobby;
        }

        public IGameFlowState TransitionTo(GameFlowStateType type)
        {
            if (CanTransitionTo(type))
            {
                return new GameFlowLobbyState();
            }
            
            Debug.LogError($"Tried to transition from {Type} to {type} and it's not allowed");
            return this;
        }

        public void StateEnter()
        {

        }
    }
}