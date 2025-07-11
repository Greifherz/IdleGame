using Game.Scripts.Army;

namespace Services.EventService
{
    public class ArmyHireEvent : IArmyHireEvent
    {
        public ArmyUnitType UnitType { get; }
        public int Amount { get; }

        public ArmyHireEvent(ArmyUnitType unitType, int amount = 1)
        {
            UnitType = unitType;
            Amount = amount;
        }
        
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
    
    public partial interface IEventHandler
    {
        void Handle(IArmyHireEvent armyHireEvent);
    }
    
    public partial class BaseEventHandler : IEventHandler
    {
        public virtual void Handle(IArmyHireEvent armyHireEvent)
        {
            Decoratee?.Handle(armyHireEvent);
        }
    }
}