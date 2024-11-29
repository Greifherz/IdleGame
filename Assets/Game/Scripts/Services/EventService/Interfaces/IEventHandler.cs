using System;

namespace Services.EventService
{
    public partial interface IEventHandler
    {
        EventType HandleType { get; }
        Action<IEvent> VisitHandle { get; }
    }
}
