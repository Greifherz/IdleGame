namespace Services.EventService
{
    public class PlayerDataUpdatedEvent : IPlayerDataUpdatedEvent
    {
        public int Property { get; }

        public PlayerDataUpdatedEvent(int property)
        {
            Property = property;
        }
        
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
    
    public partial interface IEventHandler
    {
        void Handle(IPlayerDataUpdatedEvent playerDataUpdatedEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IPlayerDataUpdatedEvent playerDataUpdatedEvent)
        {
            Decoratee?.Handle(playerDataUpdatedEvent);
        }
    }
}