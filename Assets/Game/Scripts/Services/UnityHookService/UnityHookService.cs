using System;
using ServiceLocator;
using Services.EventService;
using UnityEngine;

namespace Game.Scripts.Services.UnityHookService
{
    public class UnityHookService : MonoBehaviour //No need for interface here, this is strictly Unity
    {
        private IEventService _eventService;
        
        private void Awake()
        {
            _eventService = Locator.Current.Get<IEventService>();
        }
        
        private void OnApplicationFocus(bool hasFocus)
        {
            //Best to subscribe directly to UnityEngine.Application.focusChanged
        }

        private void OnApplicationQuit()
        {
            //Best to subscribe directly to UnityEngine.Application.quitting
        }

        private void OnApplicationPause(bool pauseStatus)//REVIEW - might not need this as I think this is only relevant to the editor
        {
            _eventService?.Raise(new ApplicationPauseUnityEvent(pauseStatus),EventPipelineType.UnityPipeline);
        }
    }
}