using Game.GameFlow;
using Game.Scripts.Services.UnityHookService;
using Game.Services.AssetLoaderService;
using Services.EventService;
using Services.PersistenceService;
using Services.TickService;
using ServiceLocator;
using Services.GameDataService;
using Services.Scheduler;
using UnityEngine;

namespace Bootstrap
{
    //This is the non-Unity startup. Here the services are created, decorated and assigned to the service locator (Dependency injection in the future!) so it's available throughout the code.
    //This way there's no need to rely on Unity events for startup and MonoBehaviours can be kept at minimum, especially for logic
    public class ServiceBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            //Initialize Service Locator 
            Locator.Initialize();

            //Create Services
            
            //Create TickService
            var TickService = new GameObject("TickService").AddComponent<UnityCoroutineTickService>();
            Locator.Current.Register<ITickService>(TickService);

            TickService.gameObject.AddComponent<UnityHookService>(); //No need to register this on the locator

            //Create Persistence Service
            IPersistenceService PersistenceService = new PlayerPrefsPersistenceService();
            PersistenceService = new NonKeyCollisionDecorator(PersistenceService); //Decorating
            PersistenceService = new PlayerPrefsSaveDecorator(PersistenceService); //Decorating
            Locator.Current.Register<IPersistenceService>(PersistenceService);
            
            //Create EventService
            var EventService = new EventService();
            Locator.Current.Register<IEventService>(EventService);
            
            //Create SchedulerService
            var Scheduler = new SchedulerService();
            Locator.Current.Register<ISchedulerService>(Scheduler);
            
            //Create AssetLoaderService
            var AssetLoader = new AssetLoaderService();
            Locator.Current.Register<IAssetLoaderService>(AssetLoader);
            
            //Initialize Services
            EventService.Initialize();
            TickService.Initialize();
            Scheduler.Initialize();
            AssetLoader.Initialize();
        }
    }
}