namespace Services.EventService
{
    public class PlayerXPGainEvent : IPlayerXPGainEvent
    {
        public int Property { get; }

        public PlayerXPGainEvent(int property)
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
        void Handle(IPlayerXPGainEvent playerXPGainEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IPlayerXPGainEvent playerXPGainEvent)
        {
            Decoratee?.Handle(playerXPGainEvent);
        }
    }
}