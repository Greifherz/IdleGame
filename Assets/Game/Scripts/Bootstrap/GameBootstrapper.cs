using Game.Data.GameplayData;
using Game.GameFlow;
using Game.Scripts.Services.GameDataService;
using ServiceLocator;
using Services.GameDataService;
using UnityEngine;

namespace Bootstrap
{
    public class GameBootstrapper
    {
        private static GameFlow _GameFlowObject;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Initialize()
        {
            //Initialize Game-related services, controllers and objects
            var DatabaseProvider = new DatabaseProviderService();
            DatabaseProvider.Initialize();
            
            Locator.Current.Register<IDatabaseProviderService>(DatabaseProvider);

            _GameFlowObject = new GameFlow(); //I don't want to hold references to it but rather communicate with it only through events.
            
            _GameFlowObject.Initialize();
            
            var GameDataService = new GamePersistenceDataService();
            Locator.Current.Register<IGamePersistenceDataService>(GameDataService);
            
            var GameplayDataService = new GameplayDataService();
            Locator.Current.Register<IGameplayDataService>(GameplayDataService);
            
            GameDataService.Initialize();
        }
    }
}