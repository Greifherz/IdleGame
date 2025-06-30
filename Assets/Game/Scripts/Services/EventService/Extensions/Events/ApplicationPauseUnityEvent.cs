namespace Services.EventService
{
    public class ApplicationPauseUnityEvent : IApplicationPauseUnityEvent
    {
        public bool PauseStatus { get; }

        public ApplicationPauseUnityEvent(bool pauseStatus)
        {
            PauseStatus = pauseStatus;
        }
        
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
    
    public partial interface IEventHandler
    {
        void Handle(IApplicationPauseUnityEvent applicationPauseUnityEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IApplicationPauseUnityEvent applicationPauseUnityEvent)
        {
            Decoratee?.Handle(applicationPauseUnityEvent);
        }
    }
}