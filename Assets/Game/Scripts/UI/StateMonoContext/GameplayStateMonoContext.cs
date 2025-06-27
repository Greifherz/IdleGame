using Game.GameFlow;
using Game.UI.Aggregators;
using ServiceLocator;
using Services.EventService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    //Mono context classes are classes that don't do much. They hold references to the scene gameObjects and other view-related scripts and helpers.
    //They are the touching point between logic and view
    public class GameplayStateMonoContext : MonoBehaviour
    {
        private IEventService _eventService;
        private IEventHandler _gameFlowEventHandler;
        private IEventHandler _gameplayViewEventHandler;

        [SerializeField] private Button BackButton;
        [SerializeField] private GameObject GameplayPanel;

        [SerializeField] private Button CollectButton;
        [SerializeField] private Button HireButton;
        [SerializeField] private TextMeshProUGUI AccumulatedGoldText;
        [SerializeField] private TextMeshProUGUI MinersText;

        void Start()
        {
            _eventService = Locator.Current.Get<IEventService>();
            var UiRefService = Locator.Current.Get<IUIRefProviderService>();
            UiRefService.SetMiningView(this,new GameplayAggregatorContext(CollectButton,HireButton,AccumulatedGoldText,MinersText));
            
            _gameFlowEventHandler = new GameFlowStateEventHandle(OnGameFlowStateEvent);
            _gameplayViewEventHandler = new ViewEventHandler(OnGameplayViewUpdated);
            _eventService.RegisterListener(_gameFlowEventHandler);
            _eventService.RegisterListener(_gameplayViewEventHandler,EventPipelineType.ViewPipeline);
            gameObject.SetActive(false);
            
            SetupButtons();
        }

        private void SetupButtons()
        {
            BackButton.onClick.AddListener(TransitionBack);
        }

        private void OnGameplayViewUpdated(IViewEvent viewEvent)
        {
            
        }

        private void TransitionBack()
        {
            _eventService.Raise(new TransitionEvent(TransitionTarget.Back));
        }

        private void OnGameFlowStateEvent(IGameFlowStateEvent gameFlowStateEvent)
        {
            gameObject.SetActive(gameFlowStateEvent.GameFlowStateType == GameFlowStateType.Gameplay);
        }
    }
    
}
