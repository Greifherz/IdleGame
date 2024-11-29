using System;

namespace Services.EventService
{
    public class GameFlowStateEventHandle : BaseEventHandler
    {
        public override EventType HandleType => EventType.GameFlowState;
        private Action<IGameFlowStateEvent> _onGameFlowStateEvent;

        public GameFlowStateEventHandle(Action<IGameFlowStateEvent> onGameFlowStateEvent)
        {
            _onGameFlowStateEvent = onGameFlowStateEvent;
        }

        public GameFlowStateEventHandle(Action<IGameFlowStateEvent> onGameFlowStateEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            _onGameFlowStateEvent = onGameFlowStateEvent;
        }
        
        public override void Handle(IGameFlowStateEvent gameFlowStateEvent)
        {
            _onGameFlowStateEvent(gameFlowStateEvent);
        }
    }
}