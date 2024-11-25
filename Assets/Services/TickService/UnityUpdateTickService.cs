using System;
using UnityEngine;

namespace Services.TickService
{
    public class UnityUpdateTickService : MonoBehaviour, ITickService
    {
        private event Action OnTick = () => { };
        private event Action OnFixedTick = () => { };
        private event Action OnLateTick = () => { };
        
        private bool Running = false;

        public void Initialize()
        {
            Running = true;
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
            Action wrapper = () => { };
            wrapper = () =>
            {
                mainThreadAction();
                OnTick -= wrapper;
            };
            OnTick += wrapper;
        }

        public void RunOnLateMainThread(Action lateMainThreadAction)
        {
            OnLateTick += lateMainThreadAction;
        }

        public void RunOnFixedMainThread(Action fixedMainThreadAction)
        {
            OnFixedTick += fixedMainThreadAction;
        }

        public void Disable()
        {
            Running = false;
        }
        
        private void OnDestroy()
        {
            Running = false;
        }

        private void Update()
        {
            if (Running) OnTick();
        }

        private void LateUpdate()
        {
            if (!Running) return;
            OnLateTick();
            OnLateTick = () => { };
        }

        private void FixedUpdate()
        {
            if (!Running) return;
            OnFixedTick();
            OnFixedTick = () => { };
        }
    }
}