using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private RectTransform FillBackground;
        [SerializeField] private RectTransform InnerFill;
        [SerializeField] private TextMeshProUGUI HealthText;
        public void SetFill(float fill)
        {
            InnerFill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, FillBackground.rect.xMax*fill);
        }

        public void SetHealthText(string text)
        {
            if(string.IsNullOrEmpty(text))
                return;
            HealthText.text = text;
        }
    }
}
