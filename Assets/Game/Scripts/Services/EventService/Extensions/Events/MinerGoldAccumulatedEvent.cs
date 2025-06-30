namespace Services.EventService
{
    public class MinerGoldAccumulatedEvent : IMinerGoldAccumulatedEvent
    {
        public int GoldQuantity { get; }

        public MinerGoldAccumulatedEvent(int goldQuantity)
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
        void Handle(IMinerGoldAccumulatedEvent minerGoldAccumulatedEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IMinerGoldAccumulatedEvent minerGoldAccumulatedEvent)
        {
            Decoratee?.Handle(minerGoldAccumulatedEvent);
        }
    }
}