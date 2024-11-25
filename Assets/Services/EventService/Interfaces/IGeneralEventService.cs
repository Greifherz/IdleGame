using System;
using ServiceLocator;

namespace Services.EventService
{
    public interface IGeneralEventService : IGameService
    {
        void RegisterGeneralListener<T>(Action<T> onEvent);
        void UnregisterGeneralListener<T>(Action<T> onEvent);
        void RaiseGeneralEvent<T>(T sentEvent);
    }
}