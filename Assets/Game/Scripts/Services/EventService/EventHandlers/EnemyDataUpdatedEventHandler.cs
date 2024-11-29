using System;

namespace Services.EventService
{
    public class EnemyDataUpdatedEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.EnemyDataUpdated;
        private Action<IEnemyDataUpdatedEvent> OnEnemyDataUpdatedEvent;

        public EnemyDataUpdatedEventHandler(Action<IEnemyDataUpdatedEvent> onEnemyDataUpdatedEvent)
        {
            OnEnemyDataUpdatedEvent = onEnemyDataUpdatedEvent;
        }

        public EnemyDataUpdatedEventHandler(Action<IEnemyDataUpdatedEvent> onEnemyDataUpdatedEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnEnemyDataUpdatedEvent = onEnemyDataUpdatedEvent;
        }
        
        public override void Handle(IEnemyDataUpdatedEvent enemyDataUpdatedEvent)
        {
            OnEnemyDataUpdatedEvent(enemyDataUpdatedEvent);
        }
    }
}