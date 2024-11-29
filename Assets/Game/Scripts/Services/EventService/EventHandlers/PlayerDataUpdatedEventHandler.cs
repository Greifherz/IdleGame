using System;

namespace Services.EventService
{
    public class PlayerDataUpdatedEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.PlayerDataUpdated;
        private Action<IPlayerDataUpdatedEvent> OnPlayerDataUpdatedEvent;

        public PlayerDataUpdatedEventHandler(Action<IPlayerDataUpdatedEvent> onPlayerDataUpdatedEvent)
        {
            OnPlayerDataUpdatedEvent = onPlayerDataUpdatedEvent;
        }

        public PlayerDataUpdatedEventHandler(Action<IPlayerDataUpdatedEvent> onPlayerDataUpdatedEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnPlayerDataUpdatedEvent = onPlayerDataUpdatedEvent;
        }
        
        public override void Handle(IPlayerDataUpdatedEvent playerDataUpdatedEvent)
        {
            OnPlayerDataUpdatedEvent(playerDataUpdatedEvent);
        }
    }
}