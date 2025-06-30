using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services.ViewProvider
{
    public class IdleItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI DescriptionText;
        [SerializeField] private TextMeshProUGUI KillCountText;
        [SerializeField] private Image IconImage;
        [SerializeField] private RectTransform FillBackground;
        [SerializeField] private RectTransform InnerFill;
        [SerializeField] private RectTransform IncreaseAnimation;

        public Button ActionButton;

        public void SetFill(float fill)
        {
            InnerFill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,FillBackground.rect.xMax*fill);
        }

        public void SetNameText(string textValue)
        {
            if(string.IsNullOrEmpty(textValue))
                return;
            
            DescriptionText.text = textValue;
        }
        
        public void SetKillCountText(string textValue)
        {
            if(string.IsNullOrEmpty(textValue))
                return;
            
            KillCountText.text = textValue;
        }

        public void PlayIncreaseAnimation(Transform temporaryParent)
        {
            IncreaseAnimation.gameObject.SetActive(true);
            
            var originalParent = IncreaseAnimation.parent;
            var pos = IncreaseAnimation.transform.position;

            IncreaseAnimation.SetParent(temporaryParent,true);
            IncreaseAnimation.DOMoveY(pos.y + 50, 0.5f).SetEase(Ease.OutQuad);
            IncreaseAnimation.DOMoveX(pos.x + 10, 0.5f).OnComplete(() =>
            {
                IncreaseAnimation.gameObject.SetActive(false);
                IncreaseAnimation.SetParent(originalParent,true);
                IncreaseAnimation.position = pos;
                
            });
        }

        // private int Count = 0;
        // private int SecondCount = 0;
        // private void Update()
        // {
        //     Count++;
        //     if (Count == 10)
        //     {
        //         Count = 0;
        //         SecondCount++;
        //         SetFill(SecondCount * 0.01f);
        //         if (SecondCount == 100)
        //         {
        //             Debug.Log("Claimed");
        //             SecondCount = 0;
        //         }
        //     }
        // }
    }
}
