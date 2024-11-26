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
        private IEventService _eventService;

        private TransitionEventHandler _transitionEventHandler;

        public GameFlow()
        {
            _eventService = Locator.Current.Get<IEventService>();
        }

        public void Initialize()
        {
            _currentState = new GameStartState(_eventService);
            _currentState.StateEnter();
            
            _transitionEventHandler = new TransitionEventHandler(OnTransition);
            _eventService.RegisterListener(_transitionEventHandler);
        }

        private void TransitionStateTo(GameFlowStateType type)
        {
            _currentState = _currentState.TransitionTo(type);
            _currentState.StateEnter();
        }

        private void OnTransition(ITransitionEvent transitionEvent)
        {
            // Debug.Log($"Transition Event received - {transitionEvent.Target}");
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