using System;
using Game.Data.GameplayData;
using Game.GameFlow;
using Game.Scripts.Services.GameDataService;
using ServiceLocator;
using Services.EventService;
using Services.Scheduler;
using Services.ViewProvider;
using Services.ViewProvider.View;

namespace Game.Scripts.Army
{
    public class ArmyPresenter : IDisposable
    {
        private readonly IEventService _eventService;
        private readonly IArmyView _view;
        
        private IEventHandler _gameFlowEventHandler;
        private IEventHandler _gameplayViewEventHandler;

        private ArmyModel _armyModel;
        
        public ArmyPresenter(GameplayData gameplayData)
        {
            _armyModel = new ArmyModel(gameplayData);
            
            _eventService = Locator.Current.Get<IEventService>();
            var ViewProviderService = Locator.Current.Get<IViewProviderService>();
            _view = ViewProviderService.ArmyView;
            
            _gameFlowEventHandler = new GameFlowStateEventHandle(OnGameFlowStateEvent);
            _gameplayViewEventHandler = new ViewEventHandler(OnGameplayViewUpdated);
            _eventService.RegisterListener(_gameFlowEventHandler);
            _eventService.RegisterListener(_gameplayViewEventHandler,EventPipelineType.ViewPipeline);
        }

        private void OnGameplayViewUpdated(IViewEvent ev)
        {
            UpdateView();
        }

        public void Hire(int armyUnitType)
        {
            var cost = _armyModel.Hire((ArmyUnitType)armyUnitType);
            _eventService.Raise(new GoldChangeEvent(-cost),EventPipelineType.GameplayPipeline);
            UpdateView();
            
        }

        private void UpdateView()
        {
            var army = _armyModel.GetArmyDataOfIndex(0); //For now only works with 1 army
            var armyUnitData = _armyModel.GetArmyUnitData(army.UnitType);
                
            _view.SetUnitCount(army.Amount.ToString());
            _view.SetUnitCost(armyUnitData.CostPerUnit.ToString());
            _view.SetUnitAttack(armyUnitData.Attack.ToString());
            _view.SetUnitHealth(armyUnitData.Health.ToString());
        }
        
        private void OnGameFlowStateEvent(IGameFlowStateEvent gameFlowStateEvent)
        {
            _view.SetVisibility(gameFlowStateEvent.GameFlowStateType == GameFlowStateType.ArmyView);
        }

        public void Dispose()
        {
            _eventService.UnregisterListener(_gameFlowEventHandler);
            _eventService.UnregisterListener(_gameplayViewEventHandler,EventPipelineType.ViewPipeline);
        }
    }
}