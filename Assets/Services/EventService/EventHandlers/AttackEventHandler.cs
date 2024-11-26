using System;

namespace Services.EventService
{
    public class AttackEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.Common;
        private Action<IAttackEvent> _onAttackEvent;

        public AttackEventHandler(Action<IAttackEvent> onAttackEvent)
        {
            _onAttackEvent = onAttackEvent;
        }

        public AttackEventHandler(Action<IAttackEvent> onAttackEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            _onAttackEvent = onAttackEvent;
        }
        
        public override void Handle(IAttackEvent attackEvent)
        {
            _onAttackEvent(attackEvent);
        }
    }
}