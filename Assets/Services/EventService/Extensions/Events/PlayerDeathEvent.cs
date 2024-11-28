using Game.Data;

namespace Services.EventService
{
    public class PlayerDeathEvent : IPlayerDeathEvent
    {
        public IPlayerCharacter PlayerCharacter { get; }

        public PlayerDeathEvent(IPlayerCharacter playerCharacter)
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