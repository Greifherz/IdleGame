namespace Services.EventService
{
    public class ApplicationFocusUnityEvent : IApplicationFocusUnityEvent
    {
        public bool HasFocus { get; }

        public ApplicationFocusUnityEvent(bool hasFocus)
        {
            HasFocus = hasFocus;
        }
        
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
    
    public partial interface IEventHandler
    {
        void Handle(IApplicationFocusUnityEvent applicationFocusUnityEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IApplicationFocusUnityEvent applicationFocusUnityEvent)
        {
            Decoratee?.Handle(applicationFocusUnityEvent);
        }
    }
}