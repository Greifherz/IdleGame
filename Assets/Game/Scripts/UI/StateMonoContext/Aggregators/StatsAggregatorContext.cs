using System;
using TMPro;
using UnityEngine.UI;

namespace Game.UI.Aggregators
{
    [Serializable]
    public class StatsAggregatorContext
    {
        public Button IncreaseAttackButton;
        public Button IncreaseArmorButton;
        public Button IncreaseHealthButton;
        public Button ApplyButton;
        public Button UndoButton;

        public TextMeshProUGUI AttackValue;
        public TextMeshProUGUI ArmorValue;
        public TextMeshProUGUI HealthValue;
        public TextMeshProUGUI PointsLeftValue;
    }
}