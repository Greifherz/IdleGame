using Game.Scripts.Army;
using Game.Scripts.Mining;
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
        private IEventHandler _armyHiredEventHandler;

        public void Initialize()
        {
            _eventService = Locator.Current.Get<IEventService>();
            _gamePersistenceDataService = Locator.Current.Get<IGamePersistenceDataService>();
            GameplayData = _gamePersistenceDataService.LoadGameplayData();
            
            //TODO - Listen to better events?
            _minerGoldCollectedEventHandler = new MinerGoldCollectEventHandler(MinerGoldCollected);
            _armyHiredEventHandler = new ArmyHireEventHandler(ArmyHired);
            _eventService.RegisterListener(_minerGoldCollectedEventHandler,EventPipelineType.GameplayPipeline);
            _eventService.RegisterListener(_armyHiredEventHandler,EventPipelineType.GameplayPipeline);

            IsInit = true;
        }

        private void ArmyHired(IArmyHireEvent armyHireEvent)
        {
            _eventService.Raise(new GameplayDataPersistenceEvent(),EventPipelineType.ServicesPipeline);
        }

        public void MinerGoldCollected(IMinerGoldCollectEvent minerEvent)
        {
            GameplayData.OverallGold += minerEvent.GoldQuantity;
            _eventService.Raise(new GameplayDataPersistenceEvent(),EventPipelineType.ServicesPipeline);
        }
    }
}