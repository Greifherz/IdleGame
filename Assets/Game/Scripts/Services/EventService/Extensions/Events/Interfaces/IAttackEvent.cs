namespace Services.EventService
{
    public interface IAttackEvent : IEvent
    {
        int EnemyIndex { get; }
    }
}