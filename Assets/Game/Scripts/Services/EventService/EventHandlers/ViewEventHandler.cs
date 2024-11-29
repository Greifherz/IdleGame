using System;

namespace Services.EventService
{
    public class ViewEventHandler : BaseEventHandler
    {
        private Action<IViewEvent> _onViewEvent;

        public ViewEventHandler(Action<IViewEvent> onViewEvent)
        {
            _onViewEvent = onViewEvent;
        }

        public ViewEventHandler(Action<IViewEvent> onViewEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            _onViewEvent = onViewEvent;
        }
        
        public override void Handle(IViewEvent viewEvent)
        {
            _onViewEvent(viewEvent);
        }
    }
}