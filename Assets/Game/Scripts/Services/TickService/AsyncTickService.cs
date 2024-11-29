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
        private const int TickDeltaTimeMilisseconds = 1000 / 60; 
        
        private event Action OnTick = () => { };

        private bool _running = false;

        private UnityMainThreadProcessor _mainThreadProcessor;

        public AsyncTickService()
        {
            _mainThreadProcessor = new GameObject("MainThreadProcessor").AddComponent<UnityMainThreadProcessor>();
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
                if (change == PlayModeStateChange.ExitingPlayMode) _running = false;
            }; 
#endif
            _running = true;
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

        private async Task Clock()
        {
            _running = true;
            await Task.Run(() =>
            {
                while (_running)
                {
                    OnTick();
                    Task.Delay(TickDeltaTimeMilisseconds).Wait();
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