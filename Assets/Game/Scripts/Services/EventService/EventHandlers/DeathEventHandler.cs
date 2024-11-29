using System;

namespace Services.EventService
{
    public class DeathEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.Death;
        private Action<IDeathEvent> OnDeathEvent;

        public DeathEventHandler(Action<IDeathEvent> onDeathEvent)
        {
            OnDeathEvent = onDeathEvent;
        }

        public DeathEventHandler(Action<IDeathEvent> onDeathEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnDeathEvent = onDeathEvent;
        }
        
        public override void Handle(IDeathEvent commonEvent)
        {
            OnDeathEvent(commonEvent);
        }
    }
}