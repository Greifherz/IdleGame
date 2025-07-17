using System;
using ServiceLocator;
using Services.TickService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Services.SceneTransition
{
    public interface ISceneTransitionService : IGameService
    {
        bool IsTransitioning { get; }
        void LoadScene(string sceneName, Action onTransitionMidpoint = null);
    }
    
    public class SceneTransitionService : ISceneTransitionService, IDisposable
    {
        private enum TransitionState { Idle, FadingOut, Unloading, Loading, FadingIn }

        public bool IsTransitioning { get; private set; }

        private readonly ITickService _tickService;
        private readonly TransitionView _transitionView;

        private TransitionState _currentState = TransitionState.Idle;
        private string _sceneToLoad;
        private string _sceneToUnload;
        private Action _onMidpointAction;
        private AsyncOperation _asyncOperation;
        
        private float _fadeTimer;
        private const float FADE_DURATION = 0.5f;

        public SceneTransitionService(ITickService tickService, TransitionView transitionView)
        {
            _tickService = tickService;
            _transitionView = transitionView;
        }

        public void Initialize()
        {
            
        }

        public void LoadScene(string sceneName, Action onMidpointAction = null)
        {
            if (IsTransitioning) return;
            
            _sceneToUnload = SceneManager.GetActiveScene().name;
            IsTransitioning = true;
            _sceneToLoad = sceneName;
            _onMidpointAction = onMidpointAction;
            
            _currentState = TransitionState.FadingOut;
            _fadeTimer = 0f;
            _tickService.RegisterTick(OnTick);
        }

        private void OnTick()
        {
            // This method acts as our "Update" loop, driven by the TickService.
            switch (_currentState)
            {
                case TransitionState.FadingOut:
                    // Increment the timer
                    _fadeTimer += Time.deltaTime;
                    // Calculate the current alpha value (from 0 to 1)
                    float fadeOutAlpha = Mathf.Clamp01(_fadeTimer / FADE_DURATION);
                    // Tell the view to update its transparency
                    _transitionView.SetAlpha(fadeOutAlpha);

                    // Check if the fade out is complete
                    if (fadeOutAlpha >= 1f)
                    {
                        // Midpoint reached. Execute the cleanup/setup action provided by the GameFlow.
                        _onMidpointAction?.Invoke();
                        
                        // Start unloading the old scene asynchronously.
                        _asyncOperation = SceneManager.UnloadSceneAsync(_sceneToUnload);
                        // Move to the next state.
                        _currentState = TransitionState.Unloading;
                    }
                    break;

                case TransitionState.Unloading:
                    // Wait for the async operation to finish.
                    if (_asyncOperation.isDone)
                    {
                        // Now that the old scene is gone, start loading the new one.
                        _asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);
                        // Move to the next state.
                        _currentState = TransitionState.Loading;
                    }
                    break;

                case TransitionState.Loading:
                    // Wait for the async operation to finish.
                    if (_asyncOperation.isDone)
                    {
                        // Set the newly loaded scene as the active one. This is important for lighting and other scene settings.
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneToLoad));
                        _sceneToUnload = _sceneToLoad; // Update the "current" scene for the next transition.
                        
                        // Reset the timer and start fading back in.
                        _fadeTimer = 0f;
                        _currentState = TransitionState.FadingIn;
                    }
                    break;

                case TransitionState.FadingIn:
                    // Increment the timer
                    _fadeTimer += Time.deltaTime;
                    // Calculate the current alpha value (from 1 down to 0)
                    float fadeInAlpha = 1f - Mathf.Clamp01(_fadeTimer / FADE_DURATION);
                    // Tell the view to update its transparency
                    _transitionView.SetAlpha(fadeInAlpha);

                    // Check if the fade in is complete
                    if (fadeInAlpha <= 0f)
                    {
                        // Transition is fully complete.
                        _currentState = TransitionState.Idle;
                        IsTransitioning = false;
                        
                        // CRUCIAL: Unregister from the TickService to stop receiving updates.
                        // This ensures the service uses zero performance when idle.
                        _tickService.UnregisterTick(OnTick);
                    }
                    break;
            }
        }

        public void Dispose()
        {
            _tickService.UnregisterTick(OnTick);
        }
    }
}