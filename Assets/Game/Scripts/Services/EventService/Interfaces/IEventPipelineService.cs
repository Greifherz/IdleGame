using ServiceLocator;

namespace Services.EventService
{
    public interface IEventPipelineService : IGameService
    {
        void RegisterListener(IEventHandler eventHandler,EventPipelineType eventPipelineType = EventPipelineType.CommonPipeline);
        void UnregisterListener(IEventHandler eventHandler,EventPipelineType eventPipelineType = EventPipelineType.CommonPipeline);
        void Raise(IEvent raisedEvent,EventPipelineType eventPipelineType = EventPipelineType.CommonPipeline);
    }
}