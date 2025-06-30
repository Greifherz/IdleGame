namespace Game.Data
{
    public interface IEnemyCharacter : ICharacter
    {
        int Id { get; }
        int KillCount { get; }
        int XpReward { get; }
    }
}