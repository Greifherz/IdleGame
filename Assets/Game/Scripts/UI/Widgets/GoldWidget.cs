using Game.Data.GameplayData;
using ServiceLocator;
using Services.EventService;
using Services.Scheduler;
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
        private GoldChangeEventHandler _goldCollectedHandler;

        void Start()
        {
            var gameplayDataService = Locator.Current.Get<IGameplayDataService>();
            var GameDataHandle = Locator.Current.Get<ISchedulerService>().Schedule(() => gameplayDataService.IsReady);
            
            GameDataHandle.OnScheduleTick += () => {
                var GameData = Locator.Current.Get<IGameplayDataService>().GameplayData;
            
                _goldAmount = GameData.OverallGold;
                GoldText.text = _goldAmount.ToString();
        
                var minerCollectedHandler = new MinerGoldCollectEventHandler(UpdateValues);
                _goldCollectedHandler = new GoldChangeEventHandler(UpdateValues,minerCollectedHandler);
                var EventService = Locator.Current.Get<IEventService>();
                EventService.RegisterListener(_goldCollectedHandler,EventPipelineType.GameplayPipeline);
            };
        }

        //TODO - Change the gold change event to be unique, not 1 per source
        public void UpdateValues(IGoldChangeEvent ev)
        {
            _goldAmount += ev.GoldQuantity;
            GoldText.text = _goldAmount.ToString();//TODO - DoTween to tween this value up
        }
    }
}
