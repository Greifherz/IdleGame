using System;
using System.Collections;
using UnityEngine;

namespace Services.TickService
{
    public class UnityUpdateTickService : MonoBehaviour, ITickService
    {
        private event Action OnTick = () => { };
        private event Action OnFixedTick = () => { };
        private event Action OnLateTick = () => { };
        
        private bool _running = false;

        public void Initialize()
        {
            _running = true;
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
            Action Wrapper = () => { };
            Wrapper = () =>
            {
                mainThreadAction();
                OnTick -= Wrapper;
            };
            OnTick += Wrapper;
        }

        public void RunOnLateMainThread(Action lateMainThreadAction)
        {
            OnLateTick += lateMainThreadAction;
        }

        public void RunOnFixedMainThread(Action fixedMainThreadAction)
        {
            OnFixedTick += fixedMainThreadAction;
        }
        
        public Coroutine RunCoroutine(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }

        public void HaltCoroutine(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
        }
        
        public void Disable()
        {
            _running = false;
        }
        
        private void OnDestroy()
        {
            _running = false;
        }

        private void Update()
        {
            if (_running) OnTick();
        }

        private void LateUpdate()
        {
            if (!_running) return;
            OnLateTick();
            OnLateTick = () => { };
        }

        private void FixedUpdate()
        {
            if (!_running) return;
            OnFixedTick();
            OnFixedTick = () => { };
        }
        
#if UNITY_EDITOR
        public void ManualClock()
        {
            OnTick();
        }
#endif
    }
}