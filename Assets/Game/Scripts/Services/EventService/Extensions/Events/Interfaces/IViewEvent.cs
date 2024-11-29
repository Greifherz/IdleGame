using System;

namespace Services.EventService
{
    public partial interface IViewEvent : IEvent
    {
        ViewEventType ViewEventType { get; }
    }

    [Flags]
    public enum ViewEventType
    {
        None = 0,
        IdleItem = 1,
        PlayerHealth = 2,
        
    }
}