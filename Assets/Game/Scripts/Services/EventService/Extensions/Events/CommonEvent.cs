namespace Services.EventService
{
    public class CommonEvent : ICommonEvent
    {
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
    
    public partial interface IEventHandler
    {
        void Handle(ICommonEvent commonEvent);
    }

    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(ICommonEvent commonEvent)
        {
            Decoratee?.Handle(commonEvent);
        }
    }
}