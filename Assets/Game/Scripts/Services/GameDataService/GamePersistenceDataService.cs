using System;
using System.Collections.Generic;
using Game.Data;
using Game.Data.GameplayData;
using Game.Data.PersistentData;
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

        public GameplayPersistentData LoadPersistentGameplayData()
        {
            var Data = GameplayPersistentData.CreateDefaultPersistentData();
            if (!_persistenceService.RetrieveBool(GameplayPersistenceKeys.GAMEPLAY_DATA_KEY)) return Data;
            

            Data = new GameplayPersistentData();
            return Data;
        }

        private void PersistGameplayData()
        {
            
        }
    }
}