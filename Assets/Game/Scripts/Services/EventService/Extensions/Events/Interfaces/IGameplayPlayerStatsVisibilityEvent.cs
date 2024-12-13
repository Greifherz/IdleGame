namespace Services.EventService
{
    public interface IGameplayPlayerStatsVisibilityEvent : IEvent
    {
        bool Visibility { get; }
    }
}