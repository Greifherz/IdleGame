namespace Services.EventService
{
    public class GoldChangeEvent : IGoldChangeEvent
    {
        public int GoldQuantity { get; }

        public GoldChangeEvent(int goldQuantity)
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
        void Handle(IGoldChangeEvent goldChangeEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IGoldChangeEvent goldChangeEvent)
        {
            Decoratee?.Handle(goldChangeEvent);
        }
    }
}