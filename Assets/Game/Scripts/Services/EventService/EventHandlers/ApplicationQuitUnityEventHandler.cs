using System;

namespace Services.EventService
{
    public class ApplicationQuitUnityEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.ApplicationQuitUnity;
        private Action<IApplicationQuitUnityEvent> OnApplicationQuitUnityEvent;

        public ApplicationQuitUnityEventHandler(Action<IApplicationQuitUnityEvent> onApplicationQuitUnityEvent)
        {
            OnApplicationQuitUnityEvent = onApplicationQuitUnityEvent;
        }

        public ApplicationQuitUnityEventHandler(Action<IApplicationQuitUnityEvent> onApplicationQuitUnityEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnApplicationQuitUnityEvent = onApplicationQuitUnityEvent;
        }
        
        public override void Handle(IApplicationQuitUnityEvent applicationQuitUnityEvent)
        {
            OnApplicationQuitUnityEvent(applicationQuitUnityEvent);
        }
    }
}