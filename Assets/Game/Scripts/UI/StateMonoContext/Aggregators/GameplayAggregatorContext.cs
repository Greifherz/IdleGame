using System;
using Game.UI.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Aggregators
{
    [Serializable]
    public class GameplayAggregatorContext : IMiningView
    {
        public Button CollectButton { get; private set; }
        public Button HireButton { get; private set; }
        public TextMeshProUGUI AccumulatedGoldText { get; private set; }
        public TextMeshProUGUI MinersText { get; private set; }
        
        public GameplayAggregatorContext(Button collectButton, Button hireButton,TextMeshProUGUI accumulatedGoldText, TextMeshProUGUI minersText)
        {
            CollectButton = collectButton;
            HireButton = hireButton;
            AccumulatedGoldText = accumulatedGoldText;
            MinersText = minersText;
            
            collectButton.onClick.AddListener(() => OnCollectClicked?.Invoke());
            hireButton.onClick.AddListener(() => OnHireClicked?.Invoke());
        }

        public event Action OnCollectClicked;
        public event Action OnHireClicked;
        public void SetAccumulatedGold(string amount)
        {
            AccumulatedGoldText.text = amount;
        }

        public void SetMinerCount(string count)
        {
            MinersText.text = count;
        }

        public void SetHireButtonInteractable(bool isInteractable)
        {
            HireButton.interactable = isInteractable;
        }
    }
}