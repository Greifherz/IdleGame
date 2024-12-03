using System;
using System.Collections;
using ServiceLocator;
using UnityEngine;

namespace Services.TickService
{
    public interface ITickService : IGameService
    {
        void RegisterTick(Action tickAction);
        void UnregisterTick(Action tickAction);

        void RunOnMainThread(Action mainThreadAction);
        void RunOnLateMainThread(Action lateMainThreadAction);
        void RunOnFixedMainThread(Action fixedMainThreadAction);

        Coroutine RunCoroutine(IEnumerator coroutine);
        void HaltCoroutine(IEnumerator coroutine);

        void Disable();
#if UNITY_EDITOR
        void ManualClock();
#endif
    }
}