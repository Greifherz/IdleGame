namespace Services.EventService
{
    public class MinerGoldCollectEventEvent : IMinerGoldCollectEventEvent
    {
        public int GoldQuantity { get; }

        public MinerGoldCollectEventEvent(int goldQuantity)
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
        void Handle(IMinerGoldCollectEventEvent minerGoldCollectEventEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IMinerGoldCollectEventEvent minerGoldCollectEventEvent)
        {
            Decoratee?.Handle(minerGoldCollectEventEvent);
        }
    }
}