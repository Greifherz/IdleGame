using Game.Data;

namespace Services.EventService
{
    public class EnemyDataUpdatedEvent : IEnemyDataUpdatedEvent
    {
        public IEnemyCharacter EnemyCharacter { get; }

        public EnemyDataUpdatedEvent(IEnemyCharacter enemyCharacter)
        {
            EnemyCharacter = enemyCharacter;
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