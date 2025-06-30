using Game.Scripts.Game;
using ServiceLocator;
using Services.EventService;
using Services.GameDataService;

namespace Game.Data.GameplayData
{
    public class GameplayDataService : IGameplayDataService
    {
        public bool IsReady => IsInit;
        public bool IsInit = false;
        
        public GameplayData GameplayData { get; private set; }

        private IGamePersistenceDataService _gamePersistenceDataService;
        private IEventService _eventService;

        private IEventHandler _minerGoldCollectedEventHandler;

        private MiningPresenter _miningPresenter;

        public void Initialize()
        {
            _eventService = Locator.Current.Get<IEventService>();
            _gamePersistenceDataService = Locator.Current.Get<IGamePersistenceDataService>();
            GameplayData = _gamePersistenceDataService.LoadGameplayData();

            _miningPresenter = new MiningPresenter(GameplayData);
            
            _minerGoldCollectedEventHandler = new MinerGoldCollectEventHandler(MinerGoldCollected);
            _eventService.RegisterListener(_minerGoldCollectedEventHandler,EventPipelineType.GameplayPipeline);

            IsInit = true;
        }

        public void MinerGoldCollected(IMinerGoldCollectEvent minerEvent)
        {
            
        }
    }
}