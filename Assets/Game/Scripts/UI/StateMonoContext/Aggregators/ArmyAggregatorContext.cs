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
        public Button[] HireButtons { get; }
        public TextMeshProUGUI[] QuantityTexts { get; }
        public TextMeshProUGUI[] AttackTexts { get; }
        public TextMeshProUGUI[] HealthTexts { get; }
        public TextMeshProUGUI[] CostTexts { get; }
        
        public Button BattleButton { get; }
        
        public event Action<int> OnHireClicked;
        public event Action OnGoToBattleClicked;

        public ArmyAggregatorContext(GameObject holder,Button[] hireButtons,TextMeshProUGUI[] quantityTexts, TextMeshProUGUI[] healthTexts, TextMeshProUGUI[] attackTexts, TextMeshProUGUI[] costTexts,Button battleButton)
        {
            Holder = holder;
            HireButtons = hireButtons;
            QuantityTexts = quantityTexts;
            AttackTexts = attackTexts;
            HealthTexts = healthTexts;
            CostTexts = costTexts;
            BattleButton = battleButton;

            for (int i = 0; i < HireButtons.Length; i++)
            {
                var btn = HireButtons[i];
                int NonMutableIndex = i;
                btn.onClick.AddListener(() => { OnHireClicked?.Invoke(NonMutableIndex); });
            }
            
            battleButton.onClick.AddListener(() => OnGoToBattleClicked?.Invoke() );
        }
        
        public void SetVisibility(bool visible)
        {
            Holder.SetActive(visible);
        }

        public void SetListVisibility(int index, bool visible)
        {
            HireButtons[index].transform.parent.gameObject.SetActive(visible);
        }

        public void SetUnitAttack(int index, string amount)
        {
            AttackTexts[index].text = amount;
        }

        public void SetUnitHealth(int index, string amount)
        {
            HealthTexts[index].text = amount;
        }

        public void SetUnitCost(int index, string amount)
        {
            CostTexts[index].text = amount;
        }

        public void SetUnitCount(int index, string amount)
        {
            QuantityTexts[index].text = amount;
        }

        public void SetHireButtonInteractable(int index, bool isInteractable)
        {
            HireButtons[index].interactable = isInteractable;
        }
    }
}