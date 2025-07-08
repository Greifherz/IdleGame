using System;
using Game.GameFlow;
using ServiceLocator;
using Services.EventService;
using Services.ViewProvider;
using Services.ViewProvider.Aggregators;
using Services.ViewProvider.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmyStateMonoContext : MonoBehaviour
{
    private IEventService _eventService;
    private IEventHandler _gameFlowEventHandler;
    private IEventHandler _gameplayViewEventHandler;
    
    [SerializeField] private Button BackButton;
    [SerializeField] private Button HireButton;

    [SerializeField] private TextMeshProUGUI SoldierQuantityText;
    [SerializeField] private TextMeshProUGUI SoldierHealthText;
    [SerializeField] private TextMeshProUGUI SoldierAttackText;

    void Start()
    {
        _eventService = Locator.Current.Get<IEventService>();
        var UiRefService = Locator.Current.Get<IViewProviderService>();
        UiRefService.SetArmyView(this,new ArmyAggregatorContext(HireButton,SoldierQuantityText,SoldierHealthText,SoldierAttackText));
            
        _gameFlowEventHandler = new GameFlowStateEventHandle(OnGameFlowStateEvent);
        _gameplayViewEventHandler = new ViewEventHandler(OnGameplayViewUpdated);
        _eventService.RegisterListener(_gameFlowEventHandler);
        _eventService.RegisterListener(_gameplayViewEventHandler,EventPipelineType.ViewPipeline);
        gameObject.SetActive(false);
            
        SetupButtons();
    }

    private void SetupButtons()
    {
        BackButton.onClick.AddListener(TransitionBack);
    }

    private void OnGameplayViewUpdated(IViewEvent viewEvent)
    {
            
    }

    private void TransitionBack()
    {
        _eventService.Raise(new TransitionEvent(TransitionTarget.Back));
    }

    private void OnGameFlowStateEvent(IGameFlowStateEvent gameFlowStateEvent)
    {
        gameObject.SetActive(gameFlowStateEvent.GameFlowStateType == GameFlowStateType.ArmyView);
    }
}
