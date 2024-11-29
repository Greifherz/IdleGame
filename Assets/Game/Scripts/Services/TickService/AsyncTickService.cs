using System;
using System.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Services.TickService
{
    // The downside of using this tick service is that it doesn't stop when unity stops unless you stop unity completely
    // On a build, no problems, but on editor needs to cleanup
    // Another issue with this is that some things need Unity's main thread. While using this tick service we're not running in it, so this error might show up.
    public class AsyncTickService : ITickService
    {
        private const int TICK_DELTA_TIME_MILISSECONDS = 1000 / 60; 
        
        private event Action OnTick = () => { };

        private bool Running = false;

        private UnityMainThreadProcessor MainThreadProcessor;

        public AsyncTickService()
        {
            MainThreadProcessor = new GameObject("MainThreadProcessor").AddComponent<UnityMainThreadProcessor>();
        }

        public void Initialize()
        {
            Init();
        }

        public async void Init()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += change =>
            {
                if (change == PlayModeStateChange.ExitingPlayMode) Running = false;
            }; 
#endif
            Running = true;
            await Clock();
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

        private async Task Clock()
        {
            Running = true;
            await Task.Run(() =>
            {
                while (Running)
                {
                    OnTick();
                    Task.Delay(TICK_DELTA_TIME_MILISSECONDS).Wait();
                }
            });
        }
        
#if UNITY_EDITOR
        public void ManualClock()
        {
            OnTick();
        }
#endif
    }
}