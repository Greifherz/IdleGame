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
            _eventService.Raise(new ApplicationFocusUnityEvent(hasFocus),EventPipelineType.UnityPipeline);
        }

        private void OnApplicationQuit()
        {
            _eventService.Raise(new ApplicationQuitUnityEvent(),EventPipelineType.UnityPipeline);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            _eventService.Raise(new ApplicationPauseUnityEvent(pauseStatus),EventPipelineType.UnityPipeline);
        }
    }
}