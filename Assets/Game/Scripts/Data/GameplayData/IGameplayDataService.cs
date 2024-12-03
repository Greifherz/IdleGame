using ServiceLocator;

namespace Game.Data.GameplayData
{
    public interface IGameplayDataService : IGameService
    {
        bool IsReady { get; }
        int EnemyCount { get; }
        EnemyData GetEnemyData(int id);
    }
}