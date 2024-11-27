namespace Services.EventService
{
    public class PlayerDeathEvent : IPlayerDeathEvent
    {
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
    
    public partial interface IEventHandler
    {
        void Handle(IPlayerDeathEvent playerDeathEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IPlayerDeathEvent playerDeathEvent)
        {
            Decoratee?.Handle(playerDeathEvent);
        }
    }
}