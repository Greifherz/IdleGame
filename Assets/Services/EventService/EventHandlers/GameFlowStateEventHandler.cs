using System;

namespace Services.EventService
{
    public class GameFlowStateEventHandle : BaseEventHandler
    {
        public override EventType HandleType => EventType.GameFlowState;
        private Action<IGameFlowStateEvent> OnGameFlowStateEvent;

        public GameFlowStateEventHandle(Action<IGameFlowStateEvent> onGameFlowStateEvent)
        {
            OnGameFlowStateEvent = onGameFlowStateEvent;
        }

        public GameFlowStateEventHandle(Action<IGameFlowStateEvent> onGameFlowStateEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnGameFlowStateEvent = onGameFlowStateEvent;
        }
        
        public override void Handle(IGameFlowStateEvent gameFlowStateEvent)
        {
            OnGameFlowStateEvent(gameFlowStateEvent);
        }
    }
}