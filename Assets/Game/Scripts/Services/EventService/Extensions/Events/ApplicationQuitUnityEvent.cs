namespace Services.EventService
{
    public class ApplicationQuitUnityEvent : IApplicationQuitUnityEvent
    {
        public ApplicationQuitUnityEvent()
        {
            
        }
        
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
    
    public partial interface IEventHandler
    {
        void Handle(IApplicationQuitUnityEvent applicationQuitUnityEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IApplicationQuitUnityEvent applicationQuitUnityEvent)
        {
            Decoratee?.Handle(applicationQuitUnityEvent);
        }
    }
}