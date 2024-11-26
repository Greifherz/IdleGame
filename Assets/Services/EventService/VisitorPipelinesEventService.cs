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
        private Queue<IEvent> GameplayEventPool = new Queue<IEvent>(10);

        public VisitorPipelinesEventService()
        {
            EventPipelines.Add(EventPipelineType.CommonPipeline, (raisedEvent) => { });
            EventPipelines.Add(EventPipelineType.ViewPipeline, (raisedEvent) => { });
            EventPipelines.Add(EventPipelineType.ServicesPipeline, (raisedEvent) => { });
            EventPipelines.Add(EventPipelineType.GameplayPipeline, (raisedEvent) => { });
        }

        public void Initialize()
        {
            var TickService = ServiceLocator.Locator.Current.Get<ITickService>();
            TickService.RegisterTick(OnTick);
        }

        public void RegisterListener(IEventHandler eventHandler, EventPipelineType eventPipelineType = EventPipelineType.CommonPipeline)
        {
            EventPipelines[eventPipelineType] += eventHandler.VisitHandle;
        }

        public void UnregisterListener(IEventHandler eventHandler, EventPipelineType eventPipelineType = EventPipelineType.CommonPipeline)
        {
            EventPipelines[eventPipelineType] -= eventHandler.VisitHandle;
        }

        public void Raise(IEvent raisedEvent,EventPipelineType eventPipelineType = EventPipelineType.CommonPipeline)
        {
            switch (eventPipelineType)
            {
                case EventPipelineType.CommonPipeline:
                    CommonEventPool.Enqueue(raisedEvent);
                    break;
                case EventPipelineType.ViewPipeline:
                    ViewEventPool.Enqueue(raisedEvent);
                    break;
                case EventPipelineType.ServicesPipeline:
                    ServicesEventPool.Enqueue(raisedEvent);
                    break;
                default:
                    GameplayEventPool.Enqueue(raisedEvent);
                    break;
            }
        }

        public void OnTick() //It should go Services > Common > Gameplay > View
        {
            var poolSize = ServicesEventPool.Count;
            for (; poolSize > 0; poolSize-- )
            {
                var ServiceEvent = ServicesEventPool.Dequeue();
                EventPipelines[EventPipelineType.ServicesPipeline](ServiceEvent);
            }
            
            poolSize = CommonEventPool.Count;
            for (; poolSize > 0; poolSize-- )
            {
                var CommonEvent = CommonEventPool.Dequeue();
                EventPipelines[EventPipelineType.CommonPipeline](CommonEvent);
            }

            poolSize = GameplayEventPool.Count;
            for (; poolSize > 0; poolSize-- )
            {
                var GameplayEvent = GameplayEventPool.Dequeue();
                EventPipelines[EventPipelineType.GameplayPipeline](GameplayEvent);
            }
            
            poolSize = ViewEventPool.Count;
            for (; poolSize > 0; poolSize-- )
            {
                var ViewEvent = ViewEventPool.Dequeue();
                EventPipelines[EventPipelineType.ViewPipeline](ViewEvent);
            }
        }
    }

    public enum EventPipelineType
    {
        CommonPipeline,
        ViewPipeline,
        ServicesPipeline,
        GameplayPipeline,
    }
}