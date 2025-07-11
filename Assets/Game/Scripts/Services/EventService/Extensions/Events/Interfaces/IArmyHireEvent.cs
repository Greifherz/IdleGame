using Game.Scripts.Army;

namespace Services.EventService
{
    public interface IArmyHireEvent : IEvent
    {
        ArmyUnitType UnitType { get; }
        int Amount { get; }
    }
}