﻿using Game.GameFlow;
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
    public class BootstrapService
    {
        private static GameFlow _GameFlowObject;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Initialize()
        {
            //Initialize Service Locator //TODO - Implement a dependency injection instead and write tests
            Locator.Initialize();

            //Create Services
            
            //Create TickService
            var TickService = new GameObject("TickService").AddComponent<UnityCoroutineTickService>();
            Locator.Current.Register<ITickService>(TickService);
            
            //Create Persistence Service
            IPersistenceService PersistenceService = new PlayerPrefsPersistenceService();
            PersistenceService = new NonKeyCollisionDecorator(PersistenceService);
            Locator.Current.Register<IPersistenceService>(PersistenceService);
            
            //Create EventService
            var EventService = new EventService();
            Locator.Current.Register<IEventService>(EventService);
            
            //Create SchedulerService
            var Scheduler = new SchedulerService();
            Locator.Current.Register<ISchedulerService>(Scheduler);
            
            //Initialize Services
            EventService.Initialize();
            TickService.Initialize();
            Scheduler.Initialize();
            
            //Initialize Game-related services, controllers and objects
            _GameFlowObject = new GameFlow(); //I don't want to hold references to it but rather communicate with it only through events.
            _GameFlowObject.Initialize();
            
            var GameDataService = new GameDataService();
            Locator.Current.Register<IGameDataService>(GameDataService);
            GameDataService.Initialize();
        }
    }
}