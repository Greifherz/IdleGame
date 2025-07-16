using TMPro;
using UnityEngine;

namespace Game.Gameplay
{
    public class UnitMonoContext : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer UnitRenderer;
        [SerializeField] private TextMeshPro UnitAmount;

        public UnitAggregatorContext GetAggregatorContext()
        {
            return new UnitAggregatorContext
            {
                QuantityText = UnitAmount,
                SpriteRenderer = UnitRenderer,
                UnitTransform = transform
            };
        }
    }
}
