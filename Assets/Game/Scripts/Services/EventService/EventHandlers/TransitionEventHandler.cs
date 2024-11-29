using System;

namespace Services.EventService
{
    public class TransitionEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.Back;
        private Action<ITransitionEvent> _onTransitionEvent;

        public TransitionEventHandler(Action<ITransitionEvent> onTransitionEvent)
        {
            _onTransitionEvent = onTransitionEvent;
        }

        public TransitionEventHandler(Action<ITransitionEvent> onTransitionEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            _onTransitionEvent = onTransitionEvent;
        }
        
        public override void Handle(ITransitionEvent transitionEvent)
        {
            _onTransitionEvent(transitionEvent);
        }
    }
}