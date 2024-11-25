using System;

namespace Services.EventService
{
    public class EventService : IEventService
    {
        private IEventPipelineService _pipelineService;
        private IGeneralEventService _generalEventService;

        public void Initialize()
        {
            var pipelineService = new VisitorPipelinesEventService();
            pipelineService.Initialize();
            _pipelineService = pipelineService;
            _generalEventService = new GeneralEventService();
        }
        
        public void RegisterListener(IEventHandler eventHandler, EventPipelineType eventPipelineType = EventPipelineType.Common)
        {
            _pipelineService.RegisterListener(eventHandler, eventPipelineType);
        }

        public void UnregisterListener(IEventHandler eventHandler, EventPipelineType eventPipelineType = EventPipelineType.Common)
        {
            _pipelineService.UnregisterListener(eventHandler, eventPipelineType);
        }

        public void Raise(IEvent raisedEvent, EventPipelineType eventPipelineType = EventPipelineType.Common)
        {
            _pipelineService.Raise(raisedEvent, eventPipelineType);
        }

        public void RegisterGeneralListener<T>(Action<T> onEvent)
        {
            _generalEventService.RegisterGeneralListener(onEvent);
        }

        public void UnregisterGeneralListener<T>(Action<T> onEvent)
        {
            _generalEventService.UnregisterGeneralListener(onEvent);
        }

        public void RaiseGeneralEvent<T>(T sentEvent)
        {
            _generalEventService.RaiseGeneralEvent(sentEvent);
        }
    }
}