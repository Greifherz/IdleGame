using Game.Data;

namespace Services.EventService
{
    public interface IEnemyDataUpdatedEvent : IEvent
    {
        IEnemyCharacter EnemyCharacter { get; }
    }
}