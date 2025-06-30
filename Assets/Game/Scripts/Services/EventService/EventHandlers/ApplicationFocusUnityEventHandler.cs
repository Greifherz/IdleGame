using System;

namespace Services.EventService
{
    public class ApplicationFocusUnityEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.ApplicationFocusUnity;
        private Action<IApplicationFocusUnityEvent> OnApplicationFocusUnityEvent;

        public ApplicationFocusUnityEventHandler(Action<IApplicationFocusUnityEvent> onApplicationFocusUnityEvent)
        {
            OnApplicationFocusUnityEvent = onApplicationFocusUnityEvent;
        }

        public ApplicationFocusUnityEventHandler(Action<IApplicationFocusUnityEvent> onApplicationFocusUnityEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnApplicationFocusUnityEvent = onApplicationFocusUnityEvent;
        }
        
        public override void Handle(IApplicationFocusUnityEvent applicationFocusUnityEvent)
        {
            OnApplicationFocusUnityEvent(applicationFocusUnityEvent);
        }
    }
}