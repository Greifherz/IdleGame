namespace Services.EventService
{
    public class MinerGoldCollectEvent : IMinerGoldCollectEvent
    {
        public int GoldQuantity { get; }

        public MinerGoldCollectEvent(int goldQuantity)
        {
            GoldQuantity = goldQuantity;
        }
        
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
    
    public partial interface IEventHandler
    {
        void Handle(IMinerGoldCollectEvent minerGoldCollectEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IMinerGoldCollectEvent minerGoldCollectEvent)
        {
            Decoratee?.Handle(minerGoldCollectEvent);
        }
    }
}