using System;

namespace Services.EventService
{
    public class GoldChangeEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.GoldChange;
        private Action<IGoldChangeEvent> OnGoldChangeEvent;

        public GoldChangeEventHandler(Action<IGoldChangeEvent> onGoldChangeEvent)
        {
            OnGoldChangeEvent = onGoldChangeEvent;
        }

        public GoldChangeEventHandler(Action<IGoldChangeEvent> onGoldChangeEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnGoldChangeEvent = onGoldChangeEvent;
        }
        
        public override void Handle(IGoldChangeEvent goldChangeEvent)
        {
            OnGoldChangeEvent(goldChangeEvent);
        }
    }
}