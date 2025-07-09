using Services.ViewProvider.Aggregators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmyStateMonoContext : MonoBehaviour
{
    [SerializeField] private Button HireButton;

    [SerializeField] private TextMeshProUGUI SoldierQuantityText;
    [SerializeField] private TextMeshProUGUI SoldierHealthText;
    [SerializeField] private TextMeshProUGUI SoldierAttackText;
    [SerializeField] private TextMeshProUGUI SoldierCostText;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public ArmyAggregatorContext CreateArmyView()
    {
        return new ArmyAggregatorContext(gameObject,HireButton, SoldierQuantityText, SoldierHealthText, SoldierAttackText, SoldierCostText);
    }
}
