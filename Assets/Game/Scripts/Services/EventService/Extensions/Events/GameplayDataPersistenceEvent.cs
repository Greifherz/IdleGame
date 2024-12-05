namespace Services.EventService
{
    public class GameplayDataPersistenceEvent : IGameplayDataPersistenceEvent
    {
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
    
    public partial interface IEventHandler
    {
        void Handle(IGameplayDataPersistenceEvent gameplayDataPersistenceEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IGameplayDataPersistenceEvent gameplayDataPersistenceEvent)
        {
            Decoratee?.Handle(gameplayDataPersistenceEvent);
        }
    }
}