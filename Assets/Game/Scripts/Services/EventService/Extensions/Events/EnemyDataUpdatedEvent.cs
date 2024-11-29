namespace Services.EventService
{
    public class EnemyDataUpdatedEvent : IEnemyDataUpdatedEvent
    {
        public int Property { get; }

        public EnemyDataUpdatedEvent(int property)
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
        void Handle(IEnemyDataUpdatedEvent enemyDataUpdatedEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IEnemyDataUpdatedEvent enemyDataUpdatedEvent)
        {
            Decoratee?.Handle(enemyDataUpdatedEvent);
        }
    }
}