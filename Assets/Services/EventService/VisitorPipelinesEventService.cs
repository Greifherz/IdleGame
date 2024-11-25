using System;
using System.Collections;
using System.Collections.Generic;
using Services.TickService;
using UnityEngine;

namespace Services.EventService
{
    public class VisitorPipelinesEventService : IEventPipelineService
    {
        //It's useful to have multiple pipelines, so we don't have a handling overhead as the project gets bigger
        //The number 3 there is the capacity, important for memory managing. It shouldn't be hardcoded but instead the number of entries in the PipelineType enum.
        //The reason it's hardcoded is that it's faster than having a reflection call just to get that number, so if you're creating more PipelineTypes, increase it
        private Dictionary<EventPipelineType,Action<IEvent>> EventPipelines = new Dictionary<EventPipelineType, Action<IEvent>>(3);
        
        private Queue<IEvent> CommonEventPool = new Queue<IEvent>(10);
        private Queue<IEvent> ViewEventPool = new Queue<IEvent>(10);
        private Queue<IEvent> ServicesEventPool = new Queue<IEvent>(10);

        public VisitorPipelinesEventService()
        {
            EventPipelines.Add(EventPipelineType.Common, (raisedEvent) => { });
            EventPipelines.Add(EventPipelineType.View, (raisedEvent) => { });
            EventPipelines.Add(EventPipelineType.Services, (raisedEvent) => { });
        }

        public void Initialize()
        {
            var TickService = ServiceLocator.Locator.Current.Get<ITickService>();
            TickService.RegisterTick(OnTick);
        }

        public void RegisterListener(IEventHandler eventHandler, EventPipelineType eventPipelineType = EventPipelineType.Common)
        {
            EventPipelines[eventPipelineType] += eventHandler.VisitHandle;
        }

        public void UnregisterListener(IEventHandler eventHandler, EventPipelineType eventPipelineType = EventPipelineType.Common)
        {
            EventPipelines[eventPipelineType] -= eventHandler.VisitHandle;
        }

        public void Raise(IEvent raisedEvent,EventPipelineType eventPipelineType = EventPipelineType.Common)
        {
            if(eventPipelineType == EventPipelineType.Common)
                CommonEventPool.Enqueue(raisedEvent);
            else if(eventPipelineType == EventPipelineType.View)
                ViewEventPool.Enqueue(raisedEvent);
            else ServicesEventPool.Enqueue(raisedEvent);
        }

        public void OnTick()
        {
            var poolSize = CommonEventPool.Count;
            for (; poolSize > 0; poolSize-- )
            {
                var CommonEvent = CommonEventPool.Dequeue();
                EventPipelines[EventPipelineType.Common](CommonEvent);
            }
            
            poolSize = ViewEventPool.Count;
            for (; poolSize > 0; poolSize-- )
            {
                var ViewEvent = ViewEventPool.Dequeue();
                EventPipelines[EventPipelineType.View](ViewEvent);
            }
            
            poolSize = ServicesEventPool.Count;
            for (; poolSize > 0; poolSize-- )
            {
                var ServiceEvent = ServicesEventPool.Dequeue();
                EventPipelines[EventPipelineType.Services](ServiceEvent);
            }
        }
    }

    public enum EventPipelineType
    {
        Common,
        View,
        Services
    }
}