using System;

namespace Services.EventService
{
    public class EventService : IEventService
    {
        private IEventPipelineService _pipelineService;
        private IGeneralEventService _generalEventService;

        public void Initialize()
        {
            var PipelineService = new VisitorPipelinesEventService();
            PipelineService.Initialize();
            _pipelineService = PipelineService;
            _generalEventService = new GeneralEventService();
        }
        
        public void RegisterListener(IEventHandler eventHandler, EventPipelineType eventPipelineType = EventPipelineType.CommonPipeline)
        {
            _pipelineService.RegisterListener(eventHandler, eventPipelineType);
        }

        public void UnregisterListener(IEventHandler eventHandler, EventPipelineType eventPipelineType = EventPipelineType.CommonPipeline)
        {
            _pipelineService.UnregisterListener(eventHandler, eventPipelineType);
        }

        public void Raise(IEvent raisedEvent, EventPipelineType eventPipelineType = EventPipelineType.CommonPipeline)
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