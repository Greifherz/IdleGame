using System;

namespace Services.EventService
{
    public class CommonEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.Common;
        private Action<ICommonEvent> OnCommonEvent;

        public CommonEventHandler(Action<ICommonEvent> onCommonEvent)
        {
            OnCommonEvent = onCommonEvent;
        }

        public CommonEventHandler(Action<ICommonEvent> onCommonEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnCommonEvent = onCommonEvent;
        }
        
        public override void Handle(ICommonEvent commonEvent)
        {
            OnCommonEvent(commonEvent);
        }
    }
}