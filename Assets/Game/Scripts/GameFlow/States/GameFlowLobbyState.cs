using UnityEngine;

namespace Game.GameFlow
{
    public class GameFlowLobbyState : IGameFlowState
    {
        public GameFlowStateType Type => GameFlowStateType.Lobby;

        public bool CanTransitionTo(GameFlowStateType type)
        {
            return type == GameFlowStateType.Gameplay;
        }

        public IGameFlowState TransitionTo(GameFlowStateType type)
        {
            if (CanTransitionTo(type))
            {
                return new GameFlowGameplayState();
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
            
        }
    }
}