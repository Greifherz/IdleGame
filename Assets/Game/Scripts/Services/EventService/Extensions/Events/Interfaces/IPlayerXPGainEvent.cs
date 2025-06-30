namespace Services.EventService
{
    public interface IPlayerXPGainEvent : IEvent
    {
        int Property { get; }
    }
}