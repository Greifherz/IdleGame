using Game.Data.GameplayData;
using Game.GameFlow;
using Game.UI;
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
            _GameFlowObject = new GameFlow(); //I don't want to hold references to it but rather communicate with it only through events.
            
            _GameFlowObject.Initialize();
            
            var GameDataService = new GamePersistenceDataService();
            Locator.Current.Register<IGamePersistenceDataService>(GameDataService);
            
            var GameplayDataService = new GameplayDataService();
            Locator.Current.Register<IGameplayDataService>(GameplayDataService);
            
            //The game takes place in the UI, basically. So some more intensive systems with many expected changes should hold all their references and encapsulate it's logic within themselves
            //This reduces the load of trafficking events and avoids creating pipelines exclusive to inner services. It's okay to think of some modules as self-contained.
            var UIRefService = new UIRefProviderService();
            Locator.Current.Register<IUIRefProviderService>(UIRefService);
            
            GameDataService.Initialize();
        }
    }
}