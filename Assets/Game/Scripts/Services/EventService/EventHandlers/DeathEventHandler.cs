using System;

namespace Services.EventService
{
    public class DeathEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.Death;
        private Action<IDeathEvent> _onDeathEvent;

        public DeathEventHandler(Action<IDeathEvent> onDeathEvent)
        {
            _onDeathEvent = onDeathEvent;
        }

        public DeathEventHandler(Action<IDeathEvent> onDeathEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            _onDeathEvent = onDeathEvent;
        }
        
        public override void Handle(IDeathEvent commonEvent)
        {
            _onDeathEvent(commonEvent);
        }
    }
}