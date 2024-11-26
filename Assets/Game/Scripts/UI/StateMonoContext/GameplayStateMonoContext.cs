using Game.GameFlow;
using ServiceLocator;
using Services.EventService;
using UnityEngine;
using UnityEngine.UI;

//Mono context classes are classes that don't do much. They hold references to the scene gameObjects and other view-related scripts and helpers.
//They are the touching point between logic and view
public class GameplayStateMonoContext : MonoBehaviour
{
    private IEventService _eventService;
    private IEventHandler _gameFlowEventHandler;

    [SerializeField] private IdleItem[] IdleItems;
    [SerializeField] private Button BackButton;
    
    void Start()
    {
        _eventService = Locator.Current.Get<IEventService>();
        _gameFlowEventHandler = new GameFlowStateEventHandle(OnGameFlowStateEvent);
        _eventService.RegisterListener(_gameFlowEventHandler);
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
