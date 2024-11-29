namespace Services.EventService
{
    public class ServiceEvent : IServiceEvent
    {
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
    
    public partial interface IEventHandler
    {
        void Handle(IServiceEvent serviceEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IServiceEvent serviceEvent)
        {
            Decoratee?.Handle(serviceEvent);
        }
    }
}