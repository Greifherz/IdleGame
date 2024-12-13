namespace Services.EventService
{
    public class IdleItemUpdateViewEvent : IViewEvent
    {
        public int IdleItemIndex { get; private set; }
        public float FillPercentage { get; private set; }
        public string Name { get; private set; }
        public int KillCount { get; private set; }
        public bool TriggerAnimation { get; private set; }

        public ViewEventType ViewEventType => ViewEventType.IdleItem;

        public IdleItemUpdateViewEvent(int idleItemIndex, float fillPercentage,int killCount,bool triggerAnimation,string name = "")
        {
            IdleItemIndex = idleItemIndex;
            FillPercentage = fillPercentage;
            KillCount = killCount;
            Name = name;
            TriggerAnimation = TriggerAnimation;
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