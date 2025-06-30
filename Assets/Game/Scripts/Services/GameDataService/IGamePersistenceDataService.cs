using Game.Data.GameplayData;
using Game.Data.PersistentData;
using ServiceLocator;

namespace Services.GameDataService
{
    public interface IGamePersistenceDataService : IGameService
    {
        GameplayData LoadGameplayData();
    }
}