using Services.ViewProvider.Aggregators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services.ViewProvider
{
    //Mono context classes are classes that don't do much. They hold references to the scene gameObjects and other view-related scripts and helpers.
    //They are the touching point between logic and view
    public class MinerStateMonoContext : MonoBehaviour
    {
        [SerializeField] private Button CollectButton;
        [SerializeField] private Button HireButton;
        [SerializeField] private TextMeshProUGUI AccumulatedGoldText;
        [SerializeField] private TextMeshProUGUI MinersText;

        void Start()
        {
            gameObject.SetActive(false);
        }

        public MiningAggregatorContext CreateMiningView()
        {
            return new MiningAggregatorContext(gameObject,CollectButton, HireButton, AccumulatedGoldText, MinersText);
        }
    }
}
