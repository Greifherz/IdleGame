using System;

namespace Services.EventService
{
    [Flags]
    public enum EventType
    {
        None = 0,
        Common = 1,
        Service = 2,
        View = 4,
        Back = 8,
        
    }
}