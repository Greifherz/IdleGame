using ServiceLocator;
using UnityEngine;

namespace Game.Services.SceneTransition
{
    public class TransitionView : MonoBehaviour, IGameService
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Locator.Current.Register<TransitionView>(this);
            SetAlpha(0f); // Start fully transparent
        }

        /// <summary>
        /// Sets the alpha of the transition canvas and controls raycasting.
        /// This is the only public method the service will need to call.
        /// </summary>
        public void SetAlpha(float alpha)
        {
            _canvasGroup.alpha = alpha;
            _canvasGroup.blocksRaycasts = alpha > 0.1f; // Block clicks when not fully transparent
        }

        public void Initialize()
        {
            
        }
    }
}