﻿namespace Services.EventService
{
    public class ViewEvent : IViewEvent
    {
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
    
    public partial interface IEventHandler
    {
        void Handle(IViewEvent viewEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IViewEvent viewEvent)
        {
            Decoratee?.Handle(viewEvent);
        }
    }
}