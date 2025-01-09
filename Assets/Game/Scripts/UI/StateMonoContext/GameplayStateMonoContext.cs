using Game.GameFlow;
using Game.UI.Aggregators;
using ServiceLocator;
using Services.EventService;
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
        private IEventHandler _deathEventHandler;
        private IEventHandler _playerStatsEventHandler;

        [SerializeField] private PlayerHealth PlayerHealth;
        [SerializeField] private IdleItem[] IdleItems;
        [SerializeField] private Button BackButton;
        [SerializeField] private Button StatsButton;
        [SerializeField] private GameObject GameplayPanel;
        [SerializeField] private GameObject PlayerStatsPanel;
        
        [SerializeField] private StatsAggregatorContext StatsAggregation;
        
        void Start()
        {
            _eventService = Locator.Current.Get<IEventService>();
            Locator.Current.Get<IUIRefProviderService>().SetStatsAggregator(this,StatsAggregation);
            _gameFlowEventHandler = new GameFlowStateEventHandle(OnGameFlowStateEvent);
            _gameplayViewEventHandler = new ViewEventHandler(OnGameplayViewUpdated);
            _deathEventHandler = new DeathEventHandler(OnEnemyDeath);
            _playerStatsEventHandler = new GameplayPlayerStatsVisibilityEventHandler(OnPlayerStatsVisibilityEvent);
            _eventService.RegisterListener(_gameFlowEventHandler);
            _eventService.RegisterListener(_gameplayViewEventHandler,EventPipelineType.ViewPipeline);
            _eventService.RegisterListener(_deathEventHandler,EventPipelineType.ViewPipeline);
            _eventService.RegisterListener(_playerStatsEventHandler,EventPipelineType.ViewPipeline);
            gameObject.SetActive(false);
            
            SetupButtons();
        }

        private void SetupButtons()
        {
            for (var Index = 0; Index < IdleItems.Length; Index++)
            {
                var IdleItem = IdleItems[Index];
                var CurrentIndex = Index;
                IdleItem.ActionButton.onClick.AddListener(() => { OnItemClicked(CurrentIndex); });
            }

            BackButton.onClick.AddListener(TransitionBack);
            StatsButton.onClick.AddListener(OpenPlayerStats);
        }

        private void OnGameplayViewUpdated(IViewEvent viewEvent)
        {
            if (viewEvent.ViewEventType.HasFlag(ViewEventType.IdleItem))
            {
                var Concrete = viewEvent.GetIdleItemUpdateViewEvent();
                if(Concrete.IdleItemIndex >= IdleItems.Length) 
                    return;
                
                var IdleItem = IdleItems[Concrete.IdleItemIndex];
                IdleItem.gameObject.SetActive(true);
                IdleItem.SetFill(Concrete.FillPercentage);
                IdleItem.SetNameText(Concrete.Name);
                IdleItem.SetKillCountText($"Times Defeated - {Concrete.KillCount}");
            }

            if (viewEvent.ViewEventType.HasFlag(ViewEventType.PlayerHealth))
            {
                var Concrete = viewEvent.GetPlayerHealthUpdateViewEvent();
                PlayerHealth.SetFill(Concrete.FillPercentage);
                PlayerHealth.SetHealthText(Concrete.HealthText);
            }
        }

        private void OnEnemyDeath(IDeathEvent deathEvent)
        {
            IdleItems[deathEvent.DeadCharacter.Id].PlayIncreaseAnimation(transform);
        }

        private void OnItemClicked(int idleItemIndex)
        {
            _eventService.Raise(new AttackEvent(idleItemIndex),EventPipelineType.GameplayPipeline);
        }

        private void TransitionBack()
        {
            _eventService.Raise(new TransitionEvent(TransitionTarget.Back));
        }

        private void OnPlayerStatsVisibilityEvent(IGameplayPlayerStatsVisibilityEvent statsEvent)
        {
            StatsButton.gameObject.SetActive(!statsEvent.Visibility);
            GameplayPanel.SetActive(!statsEvent.Visibility);
            PlayerStatsPanel.SetActive(statsEvent.Visibility);
        }

        private void OpenPlayerStats()  
        {
            _eventService.Raise(new GameplayPlayerStatsVisibilityEvent(true),EventPipelineType.ViewPipeline);
        }

        private void UndoStats()
        {
            
        }

        private void ApplyStats()
        {
            
        }

        private void OnGameFlowStateEvent(IGameFlowStateEvent gameFlowStateEvent)
        {
            gameObject.SetActive(gameFlowStateEvent.GameFlowStateType == GameFlowStateType.Gameplay);
        }
    }
    
}
