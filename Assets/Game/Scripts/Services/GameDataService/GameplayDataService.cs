using Game.Scripts.Data;
using Game.Services.AssetLoaderService;
using ServiceLocator;
using UnityEngine;

namespace Game.Data.GameplayData
{
    public class GameplayDataService : IGameplayDataService
    {
        private GameEnemyDatabase _enemyDatabase;
        private IAssetLoaderService _assetLoaderService;

        public bool IsReady => _enemyDatabase != null;
        public int EnemyCount => _enemyDatabase.Enemies.Length;

        public void Initialize()
        {
            _assetLoaderService = Locator.Current.Get<IAssetLoaderService>();
            
            _assetLoaderService.LoadAssetAsync<GameEnemyDatabase>("GameEnemyDatabase", (database) =>
            {
                _enemyDatabase = database;
            });
        }

        public EnemyData GetEnemyData(int id)
        {
            return _enemyDatabase.Enemies[id];
        }
    }
}