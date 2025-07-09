using System;
using Game.Data.GameplayData;
using Game.GameFlow;
using Services.ViewProvider;
using Services.ViewProvider.View;
using ServiceLocator;
using Services.EventService;
using Services.Scheduler;

// Renamed from MinerLogic
namespace Game.Scripts.Mining
{
    public class MiningPresenter : IDisposable
    {
        public static float TICK_TIME = 10.0f;

        private readonly ISchedulerService _schedulerService;
        private readonly IEventService _eventService;
        
        private IEventHandler _gameFlowEventHandler;
        private IEventHandler _gameplayViewEventHandler;
        
        private readonly IMiningView _view;
        private readonly MiningModel _model;
        
        private ISchedulerHandle _currentHandle;

        public MiningPresenter(GameplayData gameplayData)
        {
            _model = new MiningModel();
            _model.LoadFrom(gameplayData);

            _schedulerService = Locator.Current.Get<ISchedulerService>();
            _eventService = Locator.Current.Get<IEventService>();
            var ViewProviderService = Locator.Current.Get<IViewProviderService>();
            _view = ViewProviderService.MiningView;
        
            _view.OnCollectClicked += Collect;
            _view.OnHireClicked += Hire;
            
            
            _gameFlowEventHandler = new GameFlowStateEventHandle(OnGameFlowStateEvent);
            _gameplayViewEventHandler = new ViewEventHandler(OnGameplayViewUpdated);
            _eventService.RegisterListener(_gameFlowEventHandler);
            _eventService.RegisterListener(_gameplayViewEventHandler,EventPipelineType.ViewPipeline);

            // Start the logic loop
            ScheduleForNextTick();
            UpdateView();
        }

        private void OnGameplayViewUpdated(IViewEvent ev)
        {
            UpdateView();
        }

        private void OnTick()
        {
            _model.AddAccumulatedGold();
            _eventService.Raise(new MinerGoldAccumulatedEvent(_model.AcumulatedGold),EventPipelineType.GameplayPipeline); // Fire event with new total
            UpdateView();
        
            ScheduleForNextTick(); // Re-schedule for the next tick
        }

        private void Hire()
        {
            if (_model.CanAffordToHire())
            {
                var HireCost = _model.CurrentHireCost;
                _model.HireMiner();
                _eventService.Raise(new GoldChangeEvent(-HireCost),EventPipelineType.GameplayPipeline);
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
            _view.SetAccumulatedGold(_model.AcumulatedGold.ToString());
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
            _eventService.UnregisterListener(_gameFlowEventHandler);
            _eventService.UnregisterListener(_gameplayViewEventHandler,EventPipelineType.ViewPipeline);
        }

        private void ScheduleForNextTick()
        {
            _currentHandle = _schedulerService.Schedule(TICK_TIME);
            _currentHandle.OnScheduleTick += OnTick;
        }
        
        private void OnGameFlowStateEvent(IGameFlowStateEvent gameFlowStateEvent)
        {
            _view.SetVisibility(gameFlowStateEvent.GameFlowStateType == GameFlowStateType.Mining);
        }
    }
}