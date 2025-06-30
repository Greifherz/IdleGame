using System;
using System.Collections.Generic;
using Services.TickService;

namespace Services.EventService
{
    public class VisitorPipelinesEventService : IEventPipelineService //TODO - Event Pooling
    {
        // This is done once at startup, so there is no performance impact.
        private static readonly int PipelineCount = Enum.GetNames(typeof(EventPipelineType)).Length;

        // The core of the event dispatch. Each pipeline has a single multicast delegate.
        private readonly Dictionary<EventPipelineType, Action<IEvent>> _eventPipelines;

        // The event queues for deferred, tick-based processing.
        private readonly Dictionary<EventPipelineType, Queue<IEvent>> _eventQueues;

        public VisitorPipelinesEventService(int defaultQueueCapacity = 25)
        {
            _eventPipelines = new Dictionary<EventPipelineType, Action<IEvent>>(PipelineCount);
            _eventQueues = new Dictionary<EventPipelineType, Queue<IEvent>>(PipelineCount);

            foreach (EventPipelineType PipelineType in Enum.GetValues(typeof(EventPipelineType)))
            {
                // Initialize with an empty action to prevent nulls.
                _eventPipelines[PipelineType] = delegate { }; 
                _eventQueues[PipelineType] = new Queue<IEvent>(defaultQueueCapacity);
            }
        }

        public void Initialize()
        {
            var TickService = ServiceLocator.Locator.Current.Get<ITickService>();
            TickService.RegisterTick(OnTick);
        }

        public void RegisterListener(IEventHandler eventHandler, EventPipelineType eventPipelineType = EventPipelineType.CommonPipeline)
        {
            _eventPipelines[eventPipelineType] += eventHandler.VisitHandle;
        }

        public void UnregisterListener(IEventHandler eventHandler, EventPipelineType eventPipelineType = EventPipelineType.CommonPipeline)
        {
            _eventPipelines[eventPipelineType] -= eventHandler.VisitHandle;
        }

        public void Raise(IEvent raisedEvent, EventPipelineType eventPipelineType = EventPipelineType.CommonPipeline)
        {
            _eventQueues[eventPipelineType].Enqueue(raisedEvent);
        }

        
        private void OnTick()
        {
            foreach (EventPipelineType PipelineType in Enum.GetValues(typeof(EventPipelineType)))
            {
                ProcessPipeline(PipelineType);
            }
        }

        private void ProcessPipeline(EventPipelineType pipelineType)
        {
            var EventQueue = _eventQueues[pipelineType];
            
            // Caching the count is important to prevent processing events that are
            // raised and queued within this same frame's processing loop.
            int EventsToProcess = EventQueue.Count;

            if (EventsToProcess == 0) return;

            var PipelineAction = _eventPipelines[pipelineType];

            for (int I = 0; I < EventsToProcess; I++)
            {
                var Ev = EventQueue.Dequeue();
                
                PipelineAction?.Invoke(Ev);
            }
        }
    }

    public enum EventPipelineType
    {
        CommonPipeline,
        ViewPipeline,
        ServicesPipeline,
        GameplayPipeline,
        UnityPipeline,
    }
}