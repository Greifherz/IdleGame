using System;
using Services.ViewProvider.View;
using TMPro;
using UnityEngine.UI;

namespace Services.ViewProvider.Aggregators
{
    public class ArmyAggregatorContext : IArmyView
    {
        public Button HireButton { get; private set; }
        public TextMeshProUGUI SoldierQuantityText{ get; private set; }
        public TextMeshProUGUI SoldierAttackText { get; private set; }
        public TextMeshProUGUI SoldierHealthText { get; private set; }
        
        public event Action OnHireClicked;

        public ArmyAggregatorContext(Button hireButton,TextMeshProUGUI soldierQuantityText, TextMeshProUGUI soldierHealthText, TextMeshProUGUI soldierAttackText)
        {
            HireButton = hireButton;
            SoldierQuantityText = soldierQuantityText;
            SoldierHealthText = soldierHealthText;
            SoldierAttackText = soldierAttackText;
            
            HireButton.onClick.AddListener(() => OnHireClicked?.Invoke());
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
            SoldierAttackText.text = amount;
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