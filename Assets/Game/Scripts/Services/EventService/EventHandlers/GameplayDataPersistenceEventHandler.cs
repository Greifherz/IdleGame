using System;

namespace Services.EventService
{
    public class GameplayDataPersistenceEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.GameplayDataPersistence;
        private Action<IGameplayDataPersistenceEvent> OnGameplayDataPersistenceEvent;

        public GameplayDataPersistenceEventHandler(Action<IGameplayDataPersistenceEvent> onGameplayDataPersistenceEvent)
        {
            OnGameplayDataPersistenceEvent = onGameplayDataPersistenceEvent;
        }

        public GameplayDataPersistenceEventHandler(Action<IGameplayDataPersistenceEvent> onGameplayDataPersistenceEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnGameplayDataPersistenceEvent = onGameplayDataPersistenceEvent;
        }
        
        public override void Handle(IGameplayDataPersistenceEvent gameplayDataPersistenceEvent)
        {
            OnGameplayDataPersistenceEvent(gameplayDataPersistenceEvent);
        }
    }
}