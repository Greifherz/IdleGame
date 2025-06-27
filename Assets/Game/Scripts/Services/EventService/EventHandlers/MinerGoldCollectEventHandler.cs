using System;

namespace Services.EventService
{
    public class MinerGoldCollectEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.MinerGoldCollectEvent;
        private Action<IMinerGoldCollectEvent> OnMinerGoldCollectEvent;

        public MinerGoldCollectEventHandler(Action<IMinerGoldCollectEvent> onMinerGoldCollectEvent)
        {
            OnMinerGoldCollectEvent = onMinerGoldCollectEvent;
        }

        public MinerGoldCollectEventHandler(Action<IMinerGoldCollectEvent> onMinerGoldCollectEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnMinerGoldCollectEvent = onMinerGoldCollectEvent;
        }
        
        public override void Handle(IMinerGoldCollectEvent minerGoldCollectEvent)
        {
            OnMinerGoldCollectEvent(minerGoldCollectEvent);
        }
    }
}