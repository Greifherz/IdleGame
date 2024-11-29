using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Services.TickService
{
    public class UnityCoroutineTickService : MonoBehaviour,ITickService
    {
        private event Action OnTick = () => { };

        private bool _running = false;
        private UnityMainThreadProcessor _mainThreadProcessor;

        public void Initialize()
        {
            _mainThreadProcessor = new GameObject("MainThreadProcessor").AddComponent<UnityMainThreadProcessor>();
            _running = true;
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
            _mainThreadProcessor.OnMainThread += mainThreadAction;
        }

        public void RunOnLateMainThread(Action lateMainThreadAction)
        {
            _mainThreadProcessor.OnLateMainThread += lateMainThreadAction;
        }

        public void RunOnFixedMainThread(Action fixedMainThreadAction)
        {
            _mainThreadProcessor.OnFixedMainThread += fixedMainThreadAction;
        }
        
        public void Disable()
        {
            _running = false;
        }
        private void OnDestroy()
        {
            _running = false;
        }

        private IEnumerator Clock()
        {
            while (_running)
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