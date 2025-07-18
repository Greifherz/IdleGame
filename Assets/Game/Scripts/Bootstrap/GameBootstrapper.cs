using Game.Data.GameplayData;
using Game.GameFlow;
using Game.Scripts.Services.GameDataService;
using Game.Services.SceneTransition;
using ServiceLocator;
using Services.EventService;
using Services.GameDataService;
using Services.TickService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bootstrap
{
    public class GameBootstrapper
    {
        private static GameFlow _GameFlowObject;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Initialize()
        {
            var TickService = Locator.Current.Get<ITickService>();
            var EventService = Locator.Current.Get<IEventService>();
            //Initialize Game-related services, controllers and objects
            var DatabaseProvider = new DatabaseProviderService();
            DatabaseProvider.Initialize();

            var TransitionView = Locator.Current.Get<TransitionView>();
            Locator.Current.Unregister<TransitionView>();
            var SceneTransitionService = new SceneTransitionService(TickService,TransitionView);
            Locator.Current.Register<ISceneTransitionService>(SceneTransitionService);
            
            Locator.Current.Register<IDatabaseProviderService>(DatabaseProvider);

            _GameFlowObject = new GameFlow(EventService,SceneTransitionService); //I don't want to hold references to it but rather communicate with it only through events.
            
            _GameFlowObject.Initialize();
            
            var PersistentGameDataService = new GamePersistenceDataService();
            Locator.Current.Register<IGamePersistenceDataService>(PersistentGameDataService);
            
            var GameplayDataService = new GameplayDataService();
            Locator.Current.Register<IGameplayDataService>(GameplayDataService);

            PersistentGameDataService.Initialize();
        }
    }
}