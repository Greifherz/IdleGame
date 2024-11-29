using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Services.TickService
{
    public class UnityCoroutineTickService : MonoBehaviour,ITickService
    {
        private event Action OnTick = () => { };

        private bool Running = false;
        private UnityMainThreadProcessor MainThreadProcessor;

        public void Initialize()
        {
            MainThreadProcessor = new GameObject("MainThreadProcessor").AddComponent<UnityMainThreadProcessor>();
            Running = true;
            StartCoroutine(Clock());
        }

        public void RegisterTick(Action tickAction)
        {
            OnTick += tickAction;
        }

        public void UnregisterTick(Action tickAction)
        {
            OnTick -= tickAction;
        }

        public void RunOnMainThread(Action mainThreadAction)
        {
            MainThreadProcessor.OnMainThread += mainThreadAction;
        }

        public void RunOnLateMainThread(Action lateMainThreadAction)
        {
            MainThreadProcessor.OnLateMainThread += lateMainThreadAction;
        }

        public void RunOnFixedMainThread(Action fixedMainThreadAction)
        {
            MainThreadProcessor.OnFixedMainThread += fixedMainThreadAction;
        }
        
        public void Disable()
        {
            Running = false;
        }
        private void OnDestroy()
        {
            Running = false;
        }

        private IEnumerator Clock()
        {
            while (Running)
            {
                OnTick();
                yield return null;
            }
        }
        
#if UNITY_EDITOR
        public void ManualClock()
        {
            OnTick();
        }
#endif
    }
}