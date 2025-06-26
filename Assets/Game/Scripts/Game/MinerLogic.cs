using System;
using ServiceLocator;
using Services.EventService;
using Services.PersistenceService;
using Services.Scheduler;

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

        private int AccumulatedGold = 0;

        public MinerLogic()
        {
            _schedulerService = Locator.Current.Get<ISchedulerService>();
            _eventService = Locator.Current.Get<IEventService>();

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
            AccumulatedGold += _miningData.ActiveMiners;
        }

        private void Collect()
        {
            var collectedGold = AccumulatedGold;
            //Gold earned event
            _eventService.Raise(new MinerGoldCollectEventEvent(collectedGold));
            AccumulatedGold = 0;
        }
    }
}