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
    [SerializeField] private Button HireButton;

    [SerializeField] private TextMeshProUGUI SoldierQuantityText;
    [SerializeField] private TextMeshProUGUI SoldierHealthText;
    [SerializeField] private TextMeshProUGUI SoldierAttackText;
    [SerializeField] private TextMeshProUGUI SoldierCostText;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public ArmyAggregatorContext CreateArmyView()
    {
        return new ArmyAggregatorContext(gameObject,HireButton, SoldierQuantityText, SoldierHealthText, SoldierAttackText, SoldierCostText);
    }

    private void OnGameplayViewUpdated(IViewEvent viewEvent)
    {
            
    }

    private void OnGameFlowStateEvent(IGameFlowStateEvent gameFlowStateEvent)
    {
        gameObject.SetActive(gameFlowStateEvent.GameFlowStateType == GameFlowStateType.ArmyView);
    }
}
