using System;

namespace Services.EventService
{
    public class PlayerLevelUpEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.PlayerLevelUp;
        private Action<IPlayerLevelUpEvent> OnPlayerLevelUpEvent;

        public PlayerLevelUpEventHandler(Action<IPlayerLevelUpEvent> onPlayerLevelUpEvent)
        {
            OnPlayerLevelUpEvent = onPlayerLevelUpEvent;
        }

        public PlayerLevelUpEventHandler(Action<IPlayerLevelUpEvent> onPlayerLevelUpEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnPlayerLevelUpEvent = onPlayerLevelUpEvent;
        }
        
        public override void Handle(IPlayerLevelUpEvent playerLevelUpEvent)
        {
            OnPlayerLevelUpEvent(playerLevelUpEvent);
        }
    }
}