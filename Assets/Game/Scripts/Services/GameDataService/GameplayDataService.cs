using System.Collections.Generic;
using Game.Data.PersistentData;
using Game.Scripts.Data;
using Game.Scripts.Game;
using Game.Services.AssetLoaderService;
using ServiceLocator;
using Services.EventService;
using Services.GameDataService;
using UnityEngine;

namespace Game.Data.GameplayData
{
    public class GameplayDataService : IGameplayDataService
    {
        public bool IsReady => IsInit;
        public bool IsInit = false;
        
        public GameplayData GameplayData { get; private set; }

        private IGamePersistenceDataService _gamePersistenceDataService;
        private IEventService _eventService;

        private IEventHandler _playerDataUpdatedEventHandler;

        private MinerLogic _minerLogic;

        public void Initialize()
        {
            _eventService = Locator.Current.Get<IEventService>();
            _gamePersistenceDataService = Locator.Current.Get<IGamePersistenceDataService>();
            
            _minerLogic = new MinerLogic();

            IsInit = true;
        }
    }
}