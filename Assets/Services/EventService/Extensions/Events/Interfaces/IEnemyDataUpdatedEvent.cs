namespace Services.EventService
{
    public interface IEnemyDataUpdatedEvent : IEvent
    {
        int Property { get; }
    }
}