namespace Services.EventService
{
    public interface IPlayerCharacterDataUpdateEvent : IEvent
    {
        int Property { get; }
    }
}