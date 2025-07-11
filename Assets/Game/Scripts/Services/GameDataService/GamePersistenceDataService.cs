using Game.Data.GameplayData;
using ServiceLocator;
using Services.EventService;
using Services.PersistenceService;
using UnityEngine;

namespace Services.GameDataService
{
    public class GamePersistenceDataService : IGamePersistenceDataService
    {
        private IPersistenceService _persistenceService;
        private IEventService _eventService;
        private IGameplayDataService _gameplayDataService;

        private IEventHandler _persistenceEventHandler;

        public void Initialize()
        {
            _persistenceService = Locator.Current.Get<IPersistenceService>();
            _eventService = Locator.Current.Get<IEventService>();
            _gameplayDataService = Locator.Current.Get<IGameplayDataService>();
            
            _persistenceEventHandler = new GameplayDataPersistenceEventHandler(OnGameplayDataPersistence);
            _eventService.RegisterListener(_persistenceEventHandler,EventPipelineType.ServicesPipeline);
        }

        private void OnGameplayDataPersistence(IGameplayDataPersistenceEvent obj)
        {
            PersistGameplayData();
        }

        public GameplayData LoadGameplayData()
        {
            if (!_persistenceService.RetrieveBool(GameplayPersistenceKeys.PERSISTENCE_KEY))
                return GameplayData.CreateDefaultGameplayData();
            var RawData = _persistenceService.RetrieveString(GameplayPersistenceKeys.PERSISTENCE_KEY);
            var GameData = JsonUtility.FromJson<GameplayData>(RawData);
            return GameData;
        }

        private void PersistGameplayData()
        {
            Debug.Log("PersistGameplayData");
            var GameplayData = _gameplayDataService.GameplayData;
            var SerializedGameplayData = JsonUtility.ToJson(GameplayData);
            _persistenceService.Persist(SerializedGameplayData,GameplayPersistenceKeys.PERSISTENCE_KEY);
        }
    }
}