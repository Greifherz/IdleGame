﻿using System;

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
        
    }
}