using Game.Data.GameplayData;
using Game.Data.PersistentData;
using ServiceLocator;

namespace Services.GameDataService
{
    public interface IGamePersistenceDataService : IGameService
    {
        GameplayPersistentData LoadPersistentGameplayData();
        bool PersistGameplayData(GameplayData toPersist);//TODO - Remove this from interface as it would be triggered by events and not direct call
    }
}