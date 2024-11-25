using ServiceLocator;

namespace Services.EventService
{
    public interface IEventPipelineService : IGameService
    {
        void RegisterListener(IEventHandler eventHandler,EventPipelineType eventPipelineType = EventPipelineType.Common);
        void UnregisterListener(IEventHandler eventHandler,EventPipelineType eventPipelineType = EventPipelineType.Common);
        void Raise(IEvent raisedEvent,EventPipelineType eventPipelineType = EventPipelineType.Common);
    }
}