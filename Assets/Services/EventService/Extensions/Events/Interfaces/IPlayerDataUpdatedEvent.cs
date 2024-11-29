namespace Services.EventService
{
    public interface IPlayerDataUpdatedEvent : IEvent
    {
        int Property { get; }
    }
}