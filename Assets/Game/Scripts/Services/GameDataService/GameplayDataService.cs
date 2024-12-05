using System.Collections.Generic;
using Game.Data.PersistentData;
using Game.Scripts.Data;
using Game.Services.AssetLoaderService;
using ServiceLocator;
using Services.EventService;
using Services.GameDataService;
using UnityEngine;

namespace Game.Data.GameplayData
{
    public class GameplayDataService : IGameplayDataService
    {
        public bool IsReady => _loadedFromPersistence && _enemyDatabase != null;
        private bool _loadedFromPersistence = false;
        public int EnemyCount => _enemyDatabase.Enemies.Length;
        public GameplayData GameplayData { get; private set; }
        
        private GameEnemyDatabase _enemyDatabase;
        
        private IAssetLoaderService _assetLoaderService;
        private IGamePersistenceDataService _gamePersistenceDataService;
        private IEventService _eventService;

        private IEventHandler _enemyDataUpdatedEventHandler;
        private IEventHandler _playerDataUpdatedEventHandler;

        public void Initialize()
        {
            _eventService = Locator.Current.Get<IEventService>();
            _assetLoaderService = Locator.Current.Get<IAssetLoaderService>();
            _gamePersistenceDataService = Locator.Current.Get<IGamePersistenceDataService>();
            _enemyDataUpdatedEventHandler = new EnemyDataUpdatedEventHandler(OnEnemyDataUpdated);
            _playerDataUpdatedEventHandler = new PlayerDataUpdatedEventHandler(OnPlayerDataUpdated);
            
            _assetLoaderService.LoadAssetAsync<GameEnemyDatabase>("GameEnemyDatabase", (database) =>
            {
                //In the future - far future - this won't be retrieved from persistence but rather a BackendService. Persistence will be treated as a fallback in case there's no connectivity
                _enemyDatabase = database;
                LoadDataFromPersistence();
            
                _eventService.RegisterListener(_enemyDataUpdatedEventHandler,EventPipelineType.GameplayPipeline);
                _eventService.RegisterListener(_playerDataUpdatedEventHandler,EventPipelineType.GameplayPipeline);
            });
        }

        private void LoadDataFromPersistence()
        {
            var PersistentData = _gamePersistenceDataService.LoadPersistentGameplayData();
            var EnemyDataList = new List<EnemyData>();

            for (var Index = 0; Index < _enemyDatabase.Enemies.Length; Index++)
            {
                var EnemyData = _enemyDatabase.Enemies[Index];
                EnemyData.PersistentData = PersistentData.GetEnemyPersistentData(Index);
                if (EnemyData.PersistentData.CurrentHealthPoints == 0) //Means couldn't get data from persistence
                {
                    EnemyData.PersistentData = new EnemyPersistentData(Index,0,EnemyData.HealthPoints);
                }
                EnemyDataList.Add(EnemyData);
            }

            GameplayData = new GameplayData
            {
                PlayerCharacter = new PlayerCharacter(PersistentData.PlayerPersistentData, (playerChar) => { }),
                EnemyData = EnemyDataList
            };
            _loadedFromPersistence = true;
        }

        public EnemyData GetEnemyData(int id)
        {
            return GameplayData.EnemyData[id];
        }

        private void OnEnemyDataUpdated(IEnemyDataUpdatedEvent enemyDataUpdatedEvent)
        {
            var UpdatedData = GameplayData.EnemyData[enemyDataUpdatedEvent.EnemyCharacter.Id];
            UpdatedData.PersistentData = new EnemyPersistentData(UpdatedData.EnemyId,enemyDataUpdatedEvent.EnemyCharacter.KillCount,enemyDataUpdatedEvent.EnemyCharacter.CurrentHealthPoints);
        }

        private void OnPlayerDataUpdated(IPlayerDataUpdatedEvent playerDataUpdatedEvent)
        {
            GameplayData.PlayerCharacter = playerDataUpdatedEvent.PlayerCharacter.GetConcrete();
        }
    }
}