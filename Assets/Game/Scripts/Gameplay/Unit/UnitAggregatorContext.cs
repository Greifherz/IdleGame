using TMPro;
using UnityEngine;

namespace Game.Gameplay
{
    public class UnitAggregatorContext
    {
        public TextMeshPro QuantityText;
        public SpriteRenderer SpriteRenderer;

        public void SetAmount(string amount)
        {
            QuantityText.text = amount;
        }

        public SpriteRenderer GetSpriteRenderer()
        {
            return SpriteRenderer;
        }
    }
}