﻿using System;
using Services.ViewProvider.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services.ViewProvider.Aggregators
{
    [Serializable]
    public class MiningAggregatorContext : IMiningView
    {
        public GameObject Holder { get; private set; }
        public Button CollectButton { get; private set; }
        public Button HireButton { get; private set; }
        public TextMeshProUGUI AccumulatedGoldText { get; private set; }
        public TextMeshProUGUI MinersText { get; private set; }
        
        public MiningAggregatorContext(GameObject holder,Button collectButton, Button hireButton,TextMeshProUGUI accumulatedGoldText, TextMeshProUGUI minersText)
        {
            Holder = holder;
            CollectButton = collectButton;
            HireButton = hireButton;
            AccumulatedGoldText = accumulatedGoldText;
            MinersText = minersText;
            
            collectButton.onClick.AddListener(() => OnCollectClicked?.Invoke());
            hireButton.onClick.AddListener(() => OnHireClicked?.Invoke());
        }

        public void SetVisibility(bool visible)
        {
            Holder.SetActive(visible);
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