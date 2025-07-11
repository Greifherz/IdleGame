using System;

namespace Services.EventService
{
    public class ArmyHireEventHandler : BaseEventHandler
    {
        public override EventType HandleType => EventType.ArmyHire;
        private Action<IArmyHireEvent> OnArmyHireEvent;

        public ArmyHireEventHandler(Action<IArmyHireEvent> onArmyHireEvent)
        {
            OnArmyHireEvent = onArmyHireEvent;
        }

        public ArmyHireEventHandler(Action<IArmyHireEvent> onArmyHireEvent,IEventHandler decoratee)
        {
            Decoratee = decoratee;
            OnArmyHireEvent = onArmyHireEvent;
        }
        
        public override void Handle(IArmyHireEvent armyHireEvent)
        {
            OnArmyHireEvent(armyHireEvent);
        }
    }
}