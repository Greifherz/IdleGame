using System;
using Game.Gameplay;
using ServiceLocator;
using Services.TickService;
using UnityEngine;

namespace Game.Gameplay.Tests
{
    public class AnimationTestScript : MonoBehaviour
    {
        public AnimationDatabase AnimationDatabase;
        public SpriteRenderer Renderer;
        public AnimationType AnimationToTest;
        public AnimationType[] AnimationsToTest;

        private SpriteAnimationPlayer _animationPlayer;
        private int _testingAnimationIndex = 0;

        // --- Test Flags ---
        public bool Reset = false;
        public bool SequenceTest = false;
        public bool InputTest = false;

        // --- NEW: State Machine Variables for InputTest ---
        enum TestAnimationState { Idle, Move, Attack, Block }
        private TestAnimationState _currentState = TestAnimationState.Idle;

        void Start()
        {
            _animationPlayer = new SpriteAnimationPlayer(Locator.Current.Get<ITickService>());
            _animationPlayer.SetRenderer(Renderer);

            // Start in the idle state for InputTest
            if (InputTest)
            {
                SetAnimationState(TestAnimationState.Idle);
            }
            else // Legacy test functionality
            {
                _animationPlayer.SetAnimation(AnimationDatabase.GetAnimationData(AnimationToTest));
                _animationPlayer.PlayAnimation();
            }
        }

        private void Update()
        {
            if (InputTest)
            {
                HandleInputAndStateTransitions();
                return; // End the update here for this mode
            }
            
            if (Reset)
            {
                Reset = false;
                _animationPlayer.SetAnimation(AnimationDatabase.GetAnimationData(AnimationToTest));
                _animationPlayer.PlayAnimation();
            }

            if (SequenceTest)
            {
                SequenceTest = false;
                _testingAnimationIndex = 0;
                PlaySequence();
            }
        }

        // This is the core of the new state machine
        private void HandleInputAndStateTransitions()
        {
            // Do not interrupt an attack animation
            if (_currentState == TestAnimationState.Attack)
            {
                return;
            }

            // Check for attacks first, as they have priority over movement
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetAnimationState(TestAnimationState.Attack);
                return;
            }

            // Check for movement input
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
            {
                SetAnimationState(TestAnimationState.Move);
            }
            else // No movement input, go back to idle
            {
                SetAnimationState(TestAnimationState.Idle);
            }
            
            // Note: You can add the 'Block' state logic here following the same pattern.
        }

        private void SetAnimationState(TestAnimationState newState)
        {
            // Don't restart the same animation over and over
            if (_currentState == newState)
            {
                return;
            }

            _currentState = newState;

            switch (_currentState)
            {
                case TestAnimationState.Idle:
                    _animationPlayer.SetAnimation(AnimationDatabase.GetAnimationData(AnimationType.Idle));
                    _animationPlayer.PlayAnimation(); // Idle animation should loop
                    break;
                case TestAnimationState.Move:
                    _animationPlayer.SetAnimation(AnimationDatabase.GetAnimationData(AnimationType.Move));
                    _animationPlayer.PlayAnimation(); // Move animation should loop
                    break;
                case TestAnimationState.Attack:
                    _animationPlayer.SetAnimation(AnimationDatabase.GetAnimationData(AnimationType.Attack));
                    // When the one-shot attack animation is done, go back to idle.
                    _animationPlayer.SetCallback(() => SetAnimationState(TestAnimationState.Idle));
                    _animationPlayer.PlayAnimation(); // Attack animation plays once
                    break;
            }
        }
        
        // --- Sequence Test Helper ---
        private void PlaySequence()
        {
            if (_testingAnimationIndex >= AnimationsToTest.Length) return; // Sequence finished

            _animationPlayer.SetAnimation(AnimationDatabase.GetAnimationData(AnimationsToTest[_testingAnimationIndex]));
            // Set the callback to call this method again to play the next animation
            _animationPlayer.SetCallback(PlaySequence); 
            _animationPlayer.PlayAnimation();
            _testingAnimationIndex++;
        }
    }
}