using Game.Data.GameplayData;
using ServiceLocator;

namespace Services.GameDataService
{
    public interface IGameDataService : IGameService
    {
        GameplayData GameplayData { get; }
        GameplayData LoadOrCreateGameplayData();
        bool PersistGameplayData();
    }
}