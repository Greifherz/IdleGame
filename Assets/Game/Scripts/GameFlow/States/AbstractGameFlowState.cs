using System;
using Services.EventService;
using UnityEngine;

namespace Game.GameFlow
{
    public abstract class AbstractGameFlowState: IGameFlowState
    {
        public virtual GameFlowStateType Type { get; }
        protected IEventService _eventService;
        public abstract void StateEnter();

        public AbstractGameFlowState(IEventService eventService)
        {
            _eventService = eventService;
        }

        public abstract bool CanTransitionTo(GameFlowStateType type);

        public IGameFlowState TransitionTo(GameFlowStateType type)
        {
            if (CanTransitionTo(type))
            {
                StateExit();
                switch (type)
                {
                    case GameFlowStateType.Start:
                        return new GameStartState(_eventService);
                    case GameFlowStateType.Lobby:
                        return new GameFlowLobbyState(_eventService);
                    case GameFlowStateType.Mining:
                        return new GameFlowMiningState(_eventService);
                    case GameFlowStateType.ArmyView:
                        return new GameFlowArmyState(_eventService);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            
            Debug.LogError($"Tried to transition from {Type} to {type} and it's not allowed");
            return this;
        }

        protected abstract void StateExit();

        public abstract GameFlowStateType GetBackState();
    }
}