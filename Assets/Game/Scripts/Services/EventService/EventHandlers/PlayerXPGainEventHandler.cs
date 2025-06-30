using System;

namespace Services.EventService
{
    public class PlayerXPGainEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.PlayerXPGain;
        private Action<IPlayerXPGainEvent> OnPlayerXPGainEvent;

        public PlayerXPGainEventHandler(Action<IPlayerXPGainEvent> onPlayerXPGainEvent)
        {
            OnPlayerXPGainEvent = onPlayerXPGainEvent;
        }

        public PlayerXPGainEventHandler(Action<IPlayerXPGainEvent> onPlayerXPGainEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnPlayerXPGainEvent = onPlayerXPGainEvent;
        }
        
        public override void Handle(IPlayerXPGainEvent playerXPGainEvent)
        {
            OnPlayerXPGainEvent(playerXPGainEvent);
        }
    }
}