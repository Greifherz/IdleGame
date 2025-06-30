using System;
using Game.Data.GameplayData;
using Services.ViewProvider;
using Services.ViewProvider.View;
using ServiceLocator;
using Services.EventService;
using Services.PersistenceService;
using Services.Scheduler;

// Renamed from MinerLogic
namespace Game.Scripts.Game
{
    public class MiningPresenter : IDisposable
    {
        public static float TICK_TIME = 1.0f;

        private readonly ISchedulerService _schedulerService;
        private readonly IEventService _eventService;
        private readonly IMiningView _view;
        private readonly MiningModel _model;
        
        private ISchedulerHandle _currentHandle;

        // The Presenter gets its dependencies passed to it (Dependency Injection)
        public MiningPresenter(GameplayData gameplayData)
        {
            _model = new MiningModel();
            _model.LoadFrom(gameplayData.MiningData);

            _schedulerService = Locator.Current.Get<ISchedulerService>();
            _eventService = Locator.Current.Get<IEventService>();
            var ViewProviderService = Locator.Current.Get<IViewProviderService>();
            _view = ViewProviderService.MiningView;
        
            _view.OnCollectClicked += Collect;
            _view.OnHireClicked += Hire;

            // Start the logic loop
            ScheduleForNextTick();
            UpdateView();
        }

        private void OnTick()
        {
            _model.AddAccumulatedGold();
            _eventService.Raise(new MinerGoldAccumulatedEvent(_model.AccumulatedGold),EventPipelineType.GameplayPipeline); // Fire event with new total
            UpdateView();
        
            ScheduleForNextTick(); // Re-schedule for the next tick
        }

        private void Hire()
        {
            if (_model.CanAffordToHire())
            {
                _model.HireMiner();
                UpdateView();
            }
        }

        private void Collect()
        {
            int collectedGold = _model.CollectAllGold();
            if (collectedGold > 0)
            {
                _eventService.Raise(new MinerGoldCollectEvent(collectedGold),EventPipelineType.GameplayPipeline);
                UpdateView();
            }
        }
    
        /// <summary>
        /// A single place to push all model data to the view.
        /// </summary>
        private void UpdateView()
        {
            _view.SetAccumulatedGold(_model.AccumulatedGold.ToString());
            _view.SetMinerCount(_model.ActiveMiners.ToString());
            _view.SetHireButtonInteractable(_model.CanAffordToHire());
        }

        /// <summary>
        /// Crucial for preventing memory leaks.
        /// </summary>
        public void Dispose()
        {
            // Unsubscribe from everything
            if (_currentHandle != null)
            {
                _currentHandle.OnScheduleTick -= OnTick;
                _currentHandle = null; // Important to release the handle
            }
            _view.OnCollectClicked -= Collect;
            _view.OnHireClicked -= Hire;
        }

        private void ScheduleForNextTick()
        {
            _currentHandle = _schedulerService.Schedule(TICK_TIME);
            _currentHandle.OnScheduleTick += OnTick;
        }
    }
}