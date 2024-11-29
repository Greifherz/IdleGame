using System;

namespace Services.EventService
{
    public class CommonEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.Common;
        private Action<ICommonEvent> _onCommonEvent;

        public CommonEventHandler(Action<ICommonEvent> onCommonEvent)
        {
            _onCommonEvent = onCommonEvent;
        }

        public CommonEventHandler(Action<ICommonEvent> onCommonEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            _onCommonEvent = onCommonEvent;
        }
        
        public override void Handle(ICommonEvent commonEvent)
        {
            _onCommonEvent(commonEvent);
        }
    }
}