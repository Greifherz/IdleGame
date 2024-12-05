using Game.Data;

namespace Services.EventService
{
    public class PlayerDataUpdatedEvent : IPlayerDataUpdatedEvent
    {
        public IPlayerCharacter PlayerCharacter { get; }

        public PlayerDataUpdatedEvent(IPlayerCharacter playerCharacter)
        {
            PlayerCharacter = playerCharacter;
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