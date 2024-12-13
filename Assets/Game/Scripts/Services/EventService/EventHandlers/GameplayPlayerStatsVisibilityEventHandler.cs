using System;

namespace Services.EventService
{
    public class GameplayPlayerStatsVisibilityEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.GameplayPlayerStatsVisibility;
        private Action<IGameplayPlayerStatsVisibilityEvent> OnGameplayPlayerStatsVisibilityEvent;

        public GameplayPlayerStatsVisibilityEventHandler(Action<IGameplayPlayerStatsVisibilityEvent> onGameplayPlayerStatsVisibilityEvent)
        {
            OnGameplayPlayerStatsVisibilityEvent = onGameplayPlayerStatsVisibilityEvent;
        }

        public GameplayPlayerStatsVisibilityEventHandler(Action<IGameplayPlayerStatsVisibilityEvent> onGameplayPlayerStatsVisibilityEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnGameplayPlayerStatsVisibilityEvent = onGameplayPlayerStatsVisibilityEvent;
        }
        
        public override void Handle(IGameplayPlayerStatsVisibilityEvent gameplayPlayerStatsVisibilityEvent)
        {
            OnGameplayPlayerStatsVisibilityEvent(gameplayPlayerStatsVisibilityEvent);
        }
    }
}