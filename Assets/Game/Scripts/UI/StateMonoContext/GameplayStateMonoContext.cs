using Game.GameFlow;
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

        [SerializeField] private PlayerHealth PlayerHealth;
        [SerializeField] private IdleItem[] IdleItems;
        [SerializeField] private Button BackButton;
        
        void Start()
        {
            _eventService = Locator.Current.Get<IEventService>();
            _gameFlowEventHandler = new GameFlowStateEventHandle(OnGameFlowStateEvent);
            _gameplayViewEventHandler = new ViewEventHandler(OnGameplayViewUpdated);
            _eventService.RegisterListener(_gameFlowEventHandler);
            _eventService.RegisterListener(_gameplayViewEventHandler,EventPipelineType.ViewPipeline);
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

        private void OnItemClicked(int idleItemIndex)
        {
            _eventService.Raise(new AttackEvent(idleItemIndex),EventPipelineType.GameplayPipeline);
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
