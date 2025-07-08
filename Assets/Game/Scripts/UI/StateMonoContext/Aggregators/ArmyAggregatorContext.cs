using System;
using Services.ViewProvider.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services.ViewProvider.Aggregators
{
    public class ArmyAggregatorContext : IArmyView
    {
        public GameObject Holder { get; }
        public Button HireButton { get; }
        public TextMeshProUGUI SoldierQuantityText{ get; }
        public TextMeshProUGUI SoldierAttackText { get; }
        public TextMeshProUGUI SoldierHealthText { get; }
        public TextMeshProUGUI SoldierCostText { get; }
        
        public event Action OnHireClicked;

        public ArmyAggregatorContext(GameObject holder,Button hireButton,TextMeshProUGUI soldierQuantityText, TextMeshProUGUI soldierHealthText, TextMeshProUGUI soldierAttackText, TextMeshProUGUI soldierCostText)
        {
            Holder = holder;
            HireButton = hireButton;
            SoldierQuantityText = soldierQuantityText;
            SoldierHealthText = soldierHealthText;
            SoldierAttackText = soldierAttackText;
            SoldierCostText = soldierCostText;
            
            HireButton.onClick.AddListener(() => OnHireClicked?.Invoke());
        }

        public void SetVisibility(bool visible)
        {
            Holder.SetActive(visible);
        }

        public void SetUnitAttack(string amount)
        {
            SoldierAttackText.text = amount;
        }

        public void SetUnitHealth(string amount)
        {
            SoldierHealthText.text = amount;
        }

        public void SetUnitCost(string amount)
        {
            SoldierCostText.text = amount;
        }

        public void SetUnitCount(string count)
        {
            SoldierQuantityText.text = count;
        }

        public void SetHireButtonInteractable(bool isInteractable)
        {
            HireButton.interactable = isInteractable;
        }
    }
}