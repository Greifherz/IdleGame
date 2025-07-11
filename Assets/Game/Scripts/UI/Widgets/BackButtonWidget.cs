using System;
using ServiceLocator;
using Services.EventService;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Widgets
{
    public class BackButtonWidget : MonoBehaviour
    {
        [SerializeField] private Button _backButton;

        private IEventService _eventService;

        private void Start()
        {
            _eventService = Locator.Current.Get<IEventService>();
            Setup();
        }
        
        private void Setup()
        {
            _backButton.onClick.AddListener(TransitionBack);
        }

        private void TransitionBack()
        {
            _eventService.Raise(new TransitionEvent(TransitionTarget.Back));
        }
    }
}