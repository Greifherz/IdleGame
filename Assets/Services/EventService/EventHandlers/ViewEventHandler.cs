using System;

namespace Services.EventService
{
    public class ViewEventHandler : BaseEventHandler
    {
        private Action<IViewEvent> OnViewEvent;

        public ViewEventHandler(Action<IViewEvent> onViewEvent)
        {
            OnViewEvent = onViewEvent;
        }

        public ViewEventHandler(Action<IViewEvent> onViewEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnViewEvent = onViewEvent;
        }
        
        public override void Handle(IViewEvent viewEvent)
        {
            OnViewEvent(viewEvent);
        }
    }
}