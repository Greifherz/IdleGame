using Game.GameFlow;

namespace Services.EventService
{
    public class GameFlowStateEvent : IGameFlowStateEvent
    {
        public GameFlowStateType GameFlowStateType { get; }
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }

        public GameFlowStateEvent(GameFlowStateType type)
        {
            GameFlowStateType = type;
        }
    }
    
    public partial interface IEventHandler
    {
        void Handle(IGameFlowStateEvent gameFlowStateEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IGameFlowStateEvent gameFlowStateEvent)
        {
            Decoratee?.Handle(gameFlowStateEvent);
        }
    }
}