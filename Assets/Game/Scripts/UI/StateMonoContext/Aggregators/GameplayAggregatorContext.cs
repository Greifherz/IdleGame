using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Aggregators
{
    [Serializable]
    public class GameplayAggregatorContext
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
        }
    }
}