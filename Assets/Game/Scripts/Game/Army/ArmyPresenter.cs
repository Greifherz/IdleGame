using System;
using Game.Data.GameplayData;
using Game.GameFlow;
using ServiceLocator;
using Services.EventService;
using Services.ViewProvider;
using Services.ViewProvider.View;
using UnityEngine;

namespace Game.Scripts.Army
{
    public class ArmyPresenter : IDisposable
    {
        private readonly IEventService _eventService;
        private readonly IMultiArmyView _multiView;
        
        private IEventHandler _gameFlowEventHandler;
        private IEventHandler _gameplayViewEventHandler;

        private ArmyModel _armyModel;
        
        public ArmyPresenter(GameplayData gameplayData)
        {
            _armyModel = new ArmyModel(gameplayData);
            
            _eventService = Locator.Current.Get<IEventService>();
            var ViewProviderService = Locator.Current.Get<IViewProviderService>();
            _multiView = ViewProviderService.MultiArmyView;
            
            _gameFlowEventHandler = new GameFlowStateEventHandle(OnGameFlowStateEvent);
            _gameplayViewEventHandler = new ViewEventHandler(OnGameplayViewUpdated);
            _eventService.RegisterListener(_gameFlowEventHandler);
            _eventService.RegisterListener(_gameplayViewEventHandler,EventPipelineType.ViewPipeline);

            _multiView.OnHireClicked += OnHireClicked;
            
            UpdateView();
        }

        private void OnHireClicked(int index)
        {
            Hire(index);
        }

        private void OnGameplayViewUpdated(IViewEvent ev)
        {
            UpdateView();
        }

        public void Hire(int armyUnitType)
        {
            var type = (ArmyUnitType) armyUnitType;
            var cost = _armyModel.Hire(type);
            _eventService.Raise(new GoldChangeEvent(-cost),EventPipelineType.GameplayPipeline);
            _eventService.Raise(new ArmyHireEvent(type),EventPipelineType.GameplayPipeline);
            UpdateView();
        }

        private void UpdateView()
        {
            var armiesData = _armyModel._armiesData;
            for (var Index = 0; Index < armiesData.Count; Index++)
            {
                var data = armiesData[Index];
                var unitData = _armyModel.GetArmyUnitData(data.UnitType);
                
                _multiView.SetUnitCount(Index,data.Amount.ToString());
                _multiView.SetUnitCost(Index,unitData.CostPerUnit.ToString());
                _multiView.SetUnitAttack(Index,unitData.Attack.ToString());
                _multiView.SetUnitHealth(Index,unitData.Health.ToString());
                
                _multiView.SetHireButtonInteractable(Index,_armyModel.CanHire(data.UnitType));
            }
        }

        private void OnGameFlowStateEvent(IGameFlowStateEvent gameFlowStateEvent)
        {
            _multiView.SetVisibility(gameFlowStateEvent.GameFlowStateType == GameFlowStateType.ArmyView);
            UpdateView();
        }

        public void Dispose()
        {
            _multiView.OnHireClicked -= OnHireClicked;
            _eventService.UnregisterListener(_gameFlowEventHandler);
            _eventService.UnregisterListener(_gameplayViewEventHandler,EventPipelineType.ViewPipeline);
        }
    }
}