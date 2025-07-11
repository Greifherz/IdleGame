using Services.ViewProvider.Aggregators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmyStateMonoContext : MonoBehaviour
{
    [SerializeField] private Button[] HireButtons;
    
    [SerializeField] private TextMeshProUGUI[] QuantityTexts;
    [SerializeField] private TextMeshProUGUI[] HealthTexts;
    [SerializeField] private TextMeshProUGUI[] AttackTexts;
    [SerializeField] private TextMeshProUGUI[] CostTexts;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public MultiArmyAggregatorContext CreateArmyMultiView()
    {
        return new MultiArmyAggregatorContext(gameObject,HireButtons,QuantityTexts,HealthTexts,AttackTexts,CostTexts);
    }
}
