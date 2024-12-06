namespace Services.EventService
{
    public interface IPlayerLevelUpEvent : IEvent
    {
        int Property { get; }
    }
}