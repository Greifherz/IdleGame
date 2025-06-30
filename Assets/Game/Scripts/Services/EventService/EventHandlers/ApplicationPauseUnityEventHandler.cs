using System;

namespace Services.EventService
{
    public class ApplicationPauseUnityEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.ApplicationPauseUnity;
        private Action<IApplicationPauseUnityEvent> OnApplicationPauseUnityEvent;

        public ApplicationPauseUnityEventHandler(Action<IApplicationPauseUnityEvent> onApplicationPauseUnityEvent)
        {
            OnApplicationPauseUnityEvent = onApplicationPauseUnityEvent;
        }

        public ApplicationPauseUnityEventHandler(Action<IApplicationPauseUnityEvent> onApplicationPauseUnityEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnApplicationPauseUnityEvent = onApplicationPauseUnityEvent;
        }
        
        public override void Handle(IApplicationPauseUnityEvent applicationPauseUnityEvent)
        {
            OnApplicationPauseUnityEvent(applicationPauseUnityEvent);
        }
    }
}