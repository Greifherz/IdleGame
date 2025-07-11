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
        public abstract void StateExit();
        private GameFlowStateFactory _gameFlowStateFactory;

        public AbstractGameFlowState(GameFlowStateFactory gameFlowStateFactory,IEventService eventService)
        {
            _eventService = eventService;
            _gameFlowStateFactory = gameFlowStateFactory;
        }

        public abstract bool CanTransitionTo(GameFlowStateType type);

        public IGameFlowState TransitionTo(GameFlowStateType type)
        {
            if (CanTransitionTo(type))
            {
                return _gameFlowStateFactory.GetState(type);
            }
            
            Debug.LogError($"Tried to transition from {Type} to {type} and it's not allowed");
            return this;
        }


        public abstract GameFlowStateType GetBackState();
    }
}