namespace Services.EventService
{
    public class TransitionEvent : ITransitionEvent
    {
        public TransitionTarget Target { get; }
        
        public TransitionEvent(TransitionTarget target)
        {
            Target = target;
        }

        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }

    public partial interface IEventHandler
    {
        void Handle(ITransitionEvent transitionEvent);
    }

    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(ITransitionEvent transitionEvent)
        {
            Decoratee?.Handle(transitionEvent);
        }
    }
}