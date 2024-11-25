using System;

namespace Services.EventService
{
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual EventType HandleType => EventType.None;
        public Action<IEvent> VisitHandle => (gameEvent) => { gameEvent.Visit(this); };
        
        protected IEventHandler Decoratee = null;
    }
}