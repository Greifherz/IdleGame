using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IdleItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private Image IconImage;
    [SerializeField] private RectTransform FillBackground;
    [SerializeField] private RectTransform InnerFill;

    private float maxFillSize = 0;
    
    private void Start()
    {
        maxFillSize = FillBackground.rect.xMax;
    }

    public void SetFill(float fill)
    {
        InnerFill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,maxFillSize*fill);
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
