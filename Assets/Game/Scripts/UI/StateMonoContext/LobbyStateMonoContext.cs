using Game.GameFlow;
using ServiceLocator;
using Services.EventService;
using UnityEngine;
using UnityEngine.UI;

namespace Services.ViewProvider
{
    //Mono context classes are classes that don't do much. They hold references to the scene gameObjects and other view-related scripts and helpers.
    //They are the touching point between logic and view
    public class LobbyStateMonoContext : MonoBehaviour
    {
        private IEventService _eventService;
        private IEventHandler _stateChangeEventHandler;

        //As a personal guideline I never configure buttons through Unity's interface. It's harder to track. 
        //Instead I hold reference to the button (I might need it to enable/disable or change things about it anyway) and assign it.
        [SerializeField] private Button TransitionButton;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _eventService = Locator.Current.Get<IEventService>();
            _stateChangeEventHandler = new GameFlowStateEventHandle(OnGameFlowStateEvent);
            _eventService.RegisterListener(_stateChangeEventHandler);
            
            //As it's undefined what will finish the "Start" state, I'll start by having the start of the Lobby Mono Context do that
            _eventService.Raise(new TransitionEvent(TransitionTarget.Lobby));
            
            TransitionButton.onClick.AddListener(TransitionToGameplay);
        }

        public void TransitionToGameplay()
        {
            _eventService.Raise(new TransitionEvent(TransitionTarget.Mining));
        }

        private void OnGameFlowStateEvent(IGameFlowStateEvent gameFlowStateEvent)
        {
            gameObject.SetActive(gameFlowStateEvent.GameFlowStateType == GameFlowStateType.Lobby);
        }
    }
}
