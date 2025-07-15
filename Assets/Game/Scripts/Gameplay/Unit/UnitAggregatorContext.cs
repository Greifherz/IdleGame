using TMPro;

namespace Game.Gameplay
{
    public class UnitAggregatorContext
    {
        public TextMeshPro QuantityText;

        public void SetAmount(string amount)
        {
            QuantityText.text = amount;
        }
    }
}