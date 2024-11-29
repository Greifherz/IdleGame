using System;

namespace Services.EventService
{
    public class EnemyDataUpdatedEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.EnemyDataUpdated;
        private Action<IEnemyDataUpdatedEvent> _onEnemyDataUpdatedEvent;

        public EnemyDataUpdatedEventHandler(Action<IEnemyDataUpdatedEvent> onEnemyDataUpdatedEvent)
        {
            _onEnemyDataUpdatedEvent = onEnemyDataUpdatedEvent;
        }

        public EnemyDataUpdatedEventHandler(Action<IEnemyDataUpdatedEvent> onEnemyDataUpdatedEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            _onEnemyDataUpdatedEvent = onEnemyDataUpdatedEvent;
        }
        
        public override void Handle(IEnemyDataUpdatedEvent enemyDataUpdatedEvent)
        {
            _onEnemyDataUpdatedEvent(enemyDataUpdatedEvent);
        }
    }
}