using System;
using Services.TickService;
using UnityEngine;

namespace Game.Gameplay
{
    public class SpriteAnimationPlayer
    {
        private AnimationData _animationData;
        private SpriteRenderer _renderer;
        private readonly ITickService _tickService;

        // --- State Variables ---
        private bool _isPlaying = false;
        private bool _loop = false;
        private float _timePerFrame = 0;
        private float _currentFrameTime = 0;
        private int _currentFrameIndex = 0;

        private Action _endAnimationCallback = () => { };

        public SpriteAnimationPlayer(ITickService tickService)
        {
            _tickService = tickService;
        }

        public void SetCallback(Action callback)
        {
            _endAnimationCallback = callback;
        }

        public void SetRenderer(SpriteRenderer renderer)
        {
            _renderer = renderer;
            StopAnimation(); // Stop any current animation if the renderer changes.
        }

        public void SetAnimation(AnimationData data)
        {
            _animationData = data;
            StopAnimation(); // Stop any current animation if the data changes.
        }

        /// <summary>
        /// Starts playing the currently set animation.
        /// </summary>
        public void PlayAnimation()
        {
            if (_isPlaying)
            {
                // If it's already playing, just reset the animation from the start.
                ResetAnimationState();
                return;
            }

            if (_animationData == null || _renderer == null)
            {
                Debug.LogError("Cannot play animation. AnimationData or SpriteRenderer is not set.");
                return;
            }

            _loop = _animationData.Loop;
            ResetAnimationState();
            
            // --- FIX 1: Only register the tick handler ONCE ---
            _isPlaying = true;
            _tickService.RegisterTick(OnTick);
        }

        public void StopAnimation()
        {
            if (!_isPlaying) return;
            
            _isPlaying = false;
            _tickService.UnregisterTick(OnTick);
            var callback = _endAnimationCallback;
            _endAnimationCallback = null;
            callback?.Invoke();
        }

        private void ResetAnimationState()
        {
            _currentFrameIndex = 0;
            _currentFrameTime = 0;
            
            // Calculate the time per frame. Avoid division by zero.
            if (_animationData.AnimationSprites.Length > 0)
            {
                _timePerFrame = _animationData.AnimationTime / _animationData.AnimationSprites.Length;
            }
            else
            {
                _timePerFrame = float.MaxValue;
            }

            // Set the initial sprite immediately
            if (_renderer != null && _animationData != null && _animationData.AnimationSprites.Length > 0)
            {
                _renderer.sprite = _animationData.AnimationSprites[0];
            }
        }

        private void OnTick()
        {
            if (!_isPlaying) return;

            _currentFrameTime += Time.deltaTime;

            // Check if it's time to advance to the next frame
            if (_currentFrameTime >= _timePerFrame)
            {
                _currentFrameTime -= _timePerFrame; // Subtract instead of resetting to 0 to carry over extra time
                _currentFrameIndex++;

                // --- FIX 2: Handle animation completion and looping robustly ---
                if (_currentFrameIndex >= _animationData.AnimationSprites.Length)
                {
                    if (_loop)
                    {
                        _currentFrameIndex = 0; // Loop back to the start
                    }
                    else
                    {
                        // Animation is finished, stop and unregister
                        _currentFrameIndex = _animationData.AnimationSprites.Length - 1; // Clamp to last frame
                        StopAnimation();
                    }
                }
                
                // Update the sprite renderer with the correct frame
                _renderer.sprite = _animationData.AnimationSprites[_currentFrameIndex];
            }
        }
    }
}