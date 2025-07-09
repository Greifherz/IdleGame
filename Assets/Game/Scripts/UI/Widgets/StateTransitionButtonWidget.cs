using System;
using Game.GameFlow;
using NUnit.Framework;
using ServiceLocator;
using Services.EventService;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Widgets
{
    public class StateTransitionButtonWidget : MonoBehaviour
    {
        [SerializeField] private TransitionTarget StateToTransitionTo;
        [SerializeField] private Button ButtonComponent;

        private IEventService _eventService;

        private void Start()
        {
            _eventService = Locator.Current.Get<IEventService>();
            Setup();
        }

        private void Setup()
        {
            ButtonComponent.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _eventService.Raise(new TransitionEvent(StateToTransitionTo));
        }
    }
}