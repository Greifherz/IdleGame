using System;

namespace Services.EventService
{
    public class MinerGoldAccumulatedEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.MinerGoldAccumulated;
        private Action<IMinerGoldAccumulatedEvent> OnMinerGoldAccumulatedEvent;

        public MinerGoldAccumulatedEventHandler(Action<IMinerGoldAccumulatedEvent> onMinerGoldAccumulatedEvent)
        {
            OnMinerGoldAccumulatedEvent = onMinerGoldAccumulatedEvent;
        }

        public MinerGoldAccumulatedEventHandler(Action<IMinerGoldAccumulatedEvent> onMinerGoldAccumulatedEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnMinerGoldAccumulatedEvent = onMinerGoldAccumulatedEvent;
        }
        
        public override void Handle(IMinerGoldAccumulatedEvent minerGoldAccumulatedEvent)
        {
            OnMinerGoldAccumulatedEvent(minerGoldAccumulatedEvent);
        }
    }
}