using System;
using Game.Services.SceneTransition;
using ServiceLocator;
using Services.EventService;
using UnityEngine.SceneManagement;

namespace Game.GameFlow
{
    public class GameFlow
    {
        private IGameFlowState _currentState;
        private IEventService _eventService;
        private ISceneTransitionService _sceneTransitionService;

        private TransitionEventHandler _transitionEventHandler;

        public GameFlow(IEventService eventService,ISceneTransitionService sceneTransitionService)
        {
            _sceneTransitionService = sceneTransitionService;
            _eventService = eventService;
        }

        public void Initialize()
        {
            var Factory = new GameFlowStateFactory(_eventService);
            _currentState = Factory.GetState(GameFlowStateType.Start);
            _currentState.StateEnter();
            
            _transitionEventHandler = new TransitionEventHandler(OnTransition);
            _eventService.RegisterListener(_transitionEventHandler);
        }

        private void TransitionStateTo(GameFlowStateType type)
        {
            var NextSceneName = GetSceneBelongingToState(type);
            if (SceneManager.GetActiveScene().name != NextSceneName)
            {
                _sceneTransitionService.LoadScene(NextSceneName);
                _sceneTransitionService.OnFadeOutFinished += StateExitWrap;
                //Trick to make it fire and forget using current context - It will unsubscribe correctly after being called
                Action StateSwitchAction = () => { };
                StateSwitchAction = () =>
                {
                    _currentState = _currentState.TransitionTo(type);
                    _sceneTransitionService.OnUnloadFinished -= StateSwitchAction;
                };
                _sceneTransitionService.OnUnloadFinished += StateSwitchAction;
                _sceneTransitionService.OnLoadFinished += StateEnterWrap;
            }
            else
            {
                _currentState.StateExit();
                _currentState = _currentState.TransitionTo(type);
                _currentState.StateEnter();
            }
        }

        private void OnTransition(ITransitionEvent transitionEvent)
        {
            // Debug.Log($"Transition Event received - {transitionEvent.Target}");
            TransitionStateTo(TransitionTargetToStateType(transitionEvent.Target));
        }

        private GameFlowStateType TransitionTargetToStateType(TransitionTarget transitionEventTarget)
        {
            switch (transitionEventTarget)
            {
                case TransitionTarget.Back:
                    return _currentState.GetBackState();
                case TransitionTarget.Lobby:
                    return GameFlowStateType.Lobby;
                case TransitionTarget.Mining:
                    return GameFlowStateType.Mining;
                case TransitionTarget.ArmyView:
                    return GameFlowStateType.ArmyView;
                default:
                    throw new ArgumentOutOfRangeException(nameof(transitionEventTarget), transitionEventTarget, null);
            }
        }

        private string GetSceneBelongingToState(GameFlowStateType type)
        {
            switch (type)
            {
                case GameFlowStateType.Start:
                    return "StartScene";
                case GameFlowStateType.Lobby:
                case GameFlowStateType.Mining:
                case GameFlowStateType.ArmyView:
                    return "MainMenu";
                case GameFlowStateType.Battle:
                    return "BattleScene";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void StateExitWrap()
        {
            _currentState.StateExit();
            _sceneTransitionService.OnFadeOutFinished -= StateExitWrap;
        }
        
        private void StateEnterWrap()
        {
            _currentState.StateEnter();
            _sceneTransitionService.OnLoadFinished -= StateEnterWrap;
        }
    }
}