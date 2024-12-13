namespace Services.EventService
{
    public class GameplayPlayerStatsVisibilityEvent : IGameplayPlayerStatsVisibilityEvent
    {
        public bool Visibility { get; }

        public GameplayPlayerStatsVisibilityEvent(bool visibility)
        {
            Visibility = visibility;
        }
        
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
    
    public partial interface IEventHandler
    {
        void Handle(IGameplayPlayerStatsVisibilityEvent gameplayPlayerStatsVisibilityEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IGameplayPlayerStatsVisibilityEvent gameplayPlayerStatsVisibilityEvent)
        {
            Decoratee?.Handle(gameplayPlayerStatsVisibilityEvent);
        }
    }
}