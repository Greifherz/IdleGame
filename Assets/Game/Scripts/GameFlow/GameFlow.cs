using System;
using ServiceLocator;
using Services.EventService;
using Services.Scheduler;
using Services.TickService;
using UnityEngine;

namespace Game.GameFlow
{
    public class GameFlow
    {
        private IGameFlowState _currentState;
        private ITickService _tickService;
        private ISchedulerService _schedulerService;
        private IEventService _eventService;

        private TransitionEventHandler _transitionEventHandler;

        public GameFlow()
        {
            _tickService = Locator.Current.Get<ITickService>();
            _schedulerService = Locator.Current.Get<ISchedulerService>();
            _eventService = Locator.Current.Get<IEventService>();
        }

        public void Initialize()
        {
            _currentState = new GameStartState();
            
            _transitionEventHandler = new TransitionEventHandler(OnTransition);
        }

        public void TransitionStateTo(GameFlowStateType type)
        {
            _currentState = _currentState.TransitionTo(type);
            _currentState.StateEnter();
        }

        private void OnTransition(ITransitionEvent transitionEvent)
        {
            TransitionStateTo(TransitionTargetToStateType(transitionEvent.Target));
        }

        private GameFlowStateType TransitionTargetToStateType(TransitionTarget transitionEventTarget)
        {
            switch (transitionEventTarget)
            {
                case TransitionTarget.Back:
                    return _currentState.GetBackState();
                case TransitionTarget.Lobby:
                    return GameFlowStateType.Lobby;
                case TransitionTarget.Gameplay:
                    return GameFlowStateType.Gameplay;
                default:
                    throw new ArgumentOutOfRangeException(nameof(transitionEventTarget), transitionEventTarget, null);
            }
        }
    }
}