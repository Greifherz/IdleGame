using UnityEngine;

namespace Game.GameFlow
{
    public class GameFlowGameplayState : IGameFlowState
    {
        public GameFlowStateType Type => GameFlowStateType.Gameplay;

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

        public GameFlowStateType GetBackState()
        {
            return GameFlowStateType.Lobby;
        } 
        
        public void StateEnter()
        {
            Debug.Log($"Entered {Type} state");
        }
    }
}