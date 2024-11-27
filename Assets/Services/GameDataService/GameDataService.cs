using Game.Data.GameplayData;
using ServiceLocator;
using Services.EventService;
using Services.PersistenceService;

namespace Services.GameDataService
{
    public class GameDataService : IGameService
    {
        private IPersistenceService _persistenceService;
        private IEventService _eventService;

        private IEventHandler _deathevEventHandler;
        private IEventHandler _playerDeathEventHandler;

        private GameplayData _gameplayData;
        
        public GameDataService()
        {
            _persistenceService = Locator.Current.Get<IPersistenceService>();
            _eventService = Locator.Current.Get<IEventService>();
        }

        public void Initialize()
        {
            _gameplayData = LoadOrCreateGameplayData();
            ListenToEvents();
        }

        private GameplayData LoadOrCreateGameplayData()
        {
            var Data = new GameplayData();
            if (_persistenceService.RetrieveBool(GameplayPersistenceKeys.GAMEPLAY_DATA_KEY))
            {
                //For now do nothing
            }
            
            
            return Data;
        }

        private void ListenToEvents()
        {
            _deathevEventHandler = new DeathEventHandler(OnDeath);
            _playerDeathEventHandler = new PlayerDeathEventHandler(OnPlayerDeath);
            
            _eventService.RegisterListener(_deathevEventHandler,EventPipelineType.GameplayPipeline);
            _eventService.RegisterListener(_playerDeathEventHandler,EventPipelineType.GameplayPipeline);
        }

        private void OnPlayerDeath(IPlayerDeathEvent obj)
        {
            throw new System.NotImplementedException();
        }

        private void OnDeath(IDeathEvent obj)
        {
            throw new System.NotImplementedException();
        }
    }
}