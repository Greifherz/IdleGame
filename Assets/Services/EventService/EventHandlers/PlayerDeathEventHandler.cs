using System;

namespace Services.EventService
{
    public class PlayerDeathEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.Death;
        private Action<IPlayerDeathEvent> _onPlayerDeathEvent;

        public PlayerDeathEventHandler(Action<IPlayerDeathEvent> onPlayerDeathEvent)
        {
            _onPlayerDeathEvent = onPlayerDeathEvent;
        }

        public PlayerDeathEventHandler(Action<IPlayerDeathEvent> onPlayerDeathEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            _onPlayerDeathEvent = onPlayerDeathEvent;
        }
        
        public override void Handle(IPlayerDeathEvent commonEvent)
        {
            _onPlayerDeathEvent(commonEvent);
        }
    }
}