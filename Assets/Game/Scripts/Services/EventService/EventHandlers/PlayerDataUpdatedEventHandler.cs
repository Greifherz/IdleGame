using System;

namespace Services.EventService
{
    public class PlayerDataUpdatedEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.PlayerDataUpdated;
        private Action<IPlayerDataUpdatedEvent> _onPlayerDataUpdatedEvent;

        public PlayerDataUpdatedEventHandler(Action<IPlayerDataUpdatedEvent> onPlayerDataUpdatedEvent)
        {
            _onPlayerDataUpdatedEvent = onPlayerDataUpdatedEvent;
        }

        public PlayerDataUpdatedEventHandler(Action<IPlayerDataUpdatedEvent> onPlayerDataUpdatedEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            _onPlayerDataUpdatedEvent = onPlayerDataUpdatedEvent;
        }
        
        public override void Handle(IPlayerDataUpdatedEvent playerDataUpdatedEvent)
        {
            _onPlayerDataUpdatedEvent(playerDataUpdatedEvent);
        }
    }
}