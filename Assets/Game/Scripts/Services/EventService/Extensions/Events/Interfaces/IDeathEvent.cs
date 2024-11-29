using Game.Data;

namespace Services.EventService
{
    public interface IDeathEvent : IEvent
    {
        IEnemyCharacter DeadCharacter { get; }
    }
}