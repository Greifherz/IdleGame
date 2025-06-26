using System;

namespace Services.EventService
{
    public class MinerGoldCollectEventEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.MinerGoldCollectEvent;
        private Action<IMinerGoldCollectEventEvent> OnMinerGoldCollectEventEvent;

        public MinerGoldCollectEventEventHandler(Action<IMinerGoldCollectEventEvent> onMinerGoldCollectEventEvent)
        {
            OnMinerGoldCollectEventEvent = onMinerGoldCollectEventEvent;
        }

        public MinerGoldCollectEventEventHandler(Action<IMinerGoldCollectEventEvent> onMinerGoldCollectEventEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnMinerGoldCollectEventEvent = onMinerGoldCollectEventEvent;
        }
        
        public override void Handle(IMinerGoldCollectEventEvent minerGoldCollectEventEvent)
        {
            OnMinerGoldCollectEventEvent(minerGoldCollectEventEvent);
        }
    }
}