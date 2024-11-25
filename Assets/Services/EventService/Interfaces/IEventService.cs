using System;
using ServiceLocator;
using Services.TickService;

namespace Services.EventService
{
    public interface IEventService : IGeneralEventService, IEventPipelineService
    {
        void Initialize();
    }
}