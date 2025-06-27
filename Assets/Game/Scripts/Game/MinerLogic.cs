using System;
using Game.UI;
using ServiceLocator;
using Services.EventService;
using Services.PersistenceService;
using Services.Scheduler;
using TMPro;

namespace Game.Scripts.Game
{
    public class MinerLogic
    {
        private const float TICK_TIME = 5; //In seconds

        private ISchedulerService _schedulerService;
        private ISchedulerHandle _currentHandle;
        private IEventService _eventService;
        private IPersistenceService _persistenceService;

        private MiningData _miningData;

        private int AccumulatedGold = 50;

        private TextMeshProUGUI AccumulatedGoldText;
        private TextMeshProUGUI MinersText;

        public MinerLogic()
        {
            _schedulerService = Locator.Current.Get<ISchedulerService>();
            _eventService = Locator.Current.Get<IEventService>();
            var UiRefService = Locator.Current.Get<IUIRefProviderService>();
            _miningData = new MiningData();
            
            UiRefService.GameplayAggregatorContext.CollectButton.onClick.AddListener(Collect);
            UiRefService.GameplayAggregatorContext.HireButton.onClick.AddListener(Hire);

            AccumulatedGoldText = UiRefService.GameplayAggregatorContext.AccumulatedGoldText;
            MinersText = UiRefService.GameplayAggregatorContext.MinersText;

            AccumulatedGoldText.text = AccumulatedGold.ToString();
            MinersText.text = _miningData.ActiveMiners.ToString();

            ScheduleForNextTick();
        }

        private void ScheduleForNextTick()
        {
            _currentHandle = _schedulerService.Schedule(TICK_TIME);
            _currentHandle.OnScheduleTick += OnTick;
        }

        private void OnTick()
        {
            ScheduleForNextTick();
            
            //Add gold
            _eventService.Raise(new MinerGoldAccumulatedEvent(_miningData.ActiveMiners));
            AccumulatedGold += _miningData.ActiveMiners * _miningData.GoldPerMiner;
            AccumulatedGoldText.text = AccumulatedGold.ToString();//TODO - DOTween to animate this
        }

        private void Hire()
        {
            //For now only hire from the accumulated gold. Why? Only so I don't have to create the player data object :)
            AccumulatedGold -= 45 + _miningData.ActiveMiners * 5;
            _miningData.ActiveMiners++;
            MinersText.text = _miningData.ActiveMiners.ToString();
            AccumulatedGoldText.text = AccumulatedGold.ToString();//TODO - DOTween to animate this
        }

        private void Collect()
        {
            var collectedGold = AccumulatedGold;
            //Gold earned event
            _eventService.Raise(new MinerGoldCollectEvent(collectedGold));
            AccumulatedGold = 0;
            AccumulatedGoldText.text = AccumulatedGold.ToString();//TODO - DOTween to animate this
        }
    }
}