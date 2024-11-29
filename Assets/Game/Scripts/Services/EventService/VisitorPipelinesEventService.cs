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
        private Dictionary<EventPipelineType,Action<IEvent>> _eventPipelines = new Dictionary<EventPipelineType, Action<IEvent>>(4);
        
        //Again hardcoded capacity, but this time it's justified. If you ever get to queue up more than 10 in a frame, start thinking on making more pipelines
        private Queue<IEvent> _commonEventPool = new Queue<IEvent>(10);
        private Queue<IEvent> _viewEventPool = new Queue<IEvent>(10);
        private Queue<IEvent> _servicesEventPool = new Queue<IEvent>(10);
        private Queue<IEvent> _gameplayEventPool = new Queue<IEvent>(10);

        public VisitorPipelinesEventService()
        {
            _eventPipelines.Add(EventPipelineType.CommonPipeline, (raisedEvent) => { });
            _eventPipelines.Add(EventPipelineType.ViewPipeline, (raisedEvent) => { });
            _eventPipelines.Add(EventPipelineType.ServicesPipeline, (raisedEvent) => { });
            _eventPipelines.Add(EventPipelineType.GameplayPipeline, (raisedEvent) => { });
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

        public void Raise(IEvent raisedEvent,EventPipelineType eventPipelineType = EventPipelineType.CommonPipeline)
        {
            switch (eventPipelineType)
            {
                case EventPipelineType.CommonPipeline:
                    _commonEventPool.Enqueue(raisedEvent);
                    break;
                case EventPipelineType.ViewPipeline:
                    _viewEventPool.Enqueue(raisedEvent);
                    break;
                case EventPipelineType.ServicesPipeline:
                    _servicesEventPool.Enqueue(raisedEvent);
                    break;
                default:
                    _gameplayEventPool.Enqueue(raisedEvent);
                    break;
            }
        }

        private void OnTick() //It should go Services > Common > Gameplay > View
        {
            var PoolSize = _servicesEventPool.Count;
            for (; PoolSize > 0; PoolSize-- )
            {
                var ServiceEvent = _servicesEventPool.Dequeue();
                _eventPipelines[EventPipelineType.ServicesPipeline](ServiceEvent);
            }
            
            PoolSize = _commonEventPool.Count;
            for (; PoolSize > 0; PoolSize-- )
            {
                var CommonEvent = _commonEventPool.Dequeue();
                _eventPipelines[EventPipelineType.CommonPipeline](CommonEvent);
            }

            PoolSize = _gameplayEventPool.Count;
            for (; PoolSize > 0; PoolSize-- )
            {
                var GameplayEvent = _gameplayEventPool.Dequeue();
                _eventPipelines[EventPipelineType.GameplayPipeline](GameplayEvent);
            }
            
            PoolSize = _viewEventPool.Count;
            for (; PoolSize > 0; PoolSize-- )
            {
                var ViewEvent = _viewEventPool.Dequeue();
                _eventPipelines[EventPipelineType.ViewPipeline](ViewEvent);
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