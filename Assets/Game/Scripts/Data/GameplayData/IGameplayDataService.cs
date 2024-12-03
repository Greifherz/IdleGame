using ServiceLocator;

namespace Game.Data.GameplayData
{
    public interface IGameplayDataService : IGameService
    {
        bool IsReady { get; }
        EnemyData GetEnemyData(int id);
    }
}