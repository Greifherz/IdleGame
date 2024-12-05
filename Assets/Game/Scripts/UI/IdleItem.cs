using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class IdleItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI DescriptionText;
        [SerializeField] private TextMeshProUGUI KillCountText;
        [SerializeField] private Image IconImage;
        [SerializeField] private RectTransform FillBackground;
        [SerializeField] private RectTransform InnerFill;

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
