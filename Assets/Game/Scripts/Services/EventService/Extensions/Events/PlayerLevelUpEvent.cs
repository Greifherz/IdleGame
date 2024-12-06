namespace Services.EventService
{
    public class PlayerLevelUpEvent : IPlayerLevelUpEvent
    {
        public int Property { get; }

        public PlayerLevelUpEvent(int property)
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
        void Handle(IPlayerLevelUpEvent playerLevelUpEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IPlayerLevelUpEvent playerLevelUpEvent)
        {
            Decoratee?.Handle(playerLevelUpEvent);
        }
    }
}