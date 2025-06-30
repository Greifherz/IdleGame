using ServiceLocator;
using Services.EventService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Widgets
{
    public class GoldWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI GoldText;
        // [SerializeField] private Image GoldIcon; //TODO - Change Icon dynamically! If it's low a single coin, if it's not that low many copper coins, high gold coins, etc... 

        private int _goldAmount;
        private MinerGoldCollectEventHandler _goldCollectedHandler;

        void Start()
        {
            //TODO - Get current values proactively
        
            _goldCollectedHandler = new MinerGoldCollectEventHandler(UpdateValues);
            var EventService = Locator.Current.Get<IEventService>();
            EventService.RegisterListener(_goldCollectedHandler,EventPipelineType.GameplayPipeline);
        }

        //TODO - Change the gold change event to be unique, not 1 per source
        public void UpdateValues(IMinerGoldCollectEvent ev)
        {
            _goldAmount += ev.GoldQuantity;
            GoldText.text = _goldAmount.ToString();//TODO - DoTween to tween this value up
        }
    }
}
