namespace Services.EventService
{
    public class IdleItemUpdateViewEvent : IViewEvent
    {
        public int IdleItemIndex { get; private set; }
        public float FillPercentage { get; private set; }
        public string Name { get; private set; }

        public ViewEventType ViewEventType => ViewEventType.IdleItem;

        public IdleItemUpdateViewEvent(int idleItemIndex, float fillPercentage,string name = "")
        {
            IdleItemIndex = idleItemIndex;
            FillPercentage = fillPercentage;
            Name = name;
        }
        

        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }

        public IdleItemUpdateViewEvent GetIdleItemUpdateViewEvent()
        {
            return this;
        }
    }
    
    public partial interface IViewEvent : IEvent
    {
        virtual IdleItemUpdateViewEvent GetIdleItemUpdateViewEvent()
        {
            return null;
        }
    }
}