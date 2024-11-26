namespace Services.EventService
{
    public class AttackEvent : IAttackEvent
    {
        public int EnemyIndex { get; private set; }

        public AttackEvent(int enemyIndex)
        {
            EnemyIndex = enemyIndex;
        }
        
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }

    public partial interface IEventHandler
    {
        void Handle(IAttackEvent attackEvent);
    }

    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IAttackEvent attackEvent)
        {
            Decoratee?.Handle(attackEvent);
        }
    }
}