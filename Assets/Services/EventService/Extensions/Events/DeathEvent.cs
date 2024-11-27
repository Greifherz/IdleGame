namespace Services.EventService
{
    public class DeathEvent : IDeathEvent
    {
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
    
    public partial interface IEventHandler
    {
        void Handle(IDeathEvent deathEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IDeathEvent deathEvent)
        {
            Decoratee?.Handle(deathEvent);
        }
    }
}