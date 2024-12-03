using Game.Data.GameplayData;
using Game.GameLogic;
using ServiceLocator;
using Services.EventService;
using Services.Scheduler;
using UnityEngine;

namespace Game.GameFlow
{
    public class GameFlowGameplayState : IGameFlowState
    {
        public GameFlowStateType Type => GameFlowStateType.Gameplay;
        private IEventService _eventService;
        private ISchedulerService _schedulerService;
        private IGameplayDataService _gameplayDataService;

        private GameplayLogic _gameplayLogic;

        public GameFlowGameplayState(IEventService eventService)
        {
            _eventService = eventService;
            _schedulerService = Locator.Current.Get<ISchedulerService>();
            _gameplayDataService = Locator.Current.Get<IGameplayDataService>();
            
            _gameplayDataService.Initialize();
        }

        public bool CanTransitionTo(GameFlowStateType type)
        {
            return type == GameFlowStateType.Lobby;
        }

        public IGameFlowState TransitionTo(GameFlowStateType type)
        {
            if (CanTransitionTo(type))
            {
                return new GameFlowLobbyState(_eventService);
            }
            
            Debug.LogError($"Tried to transition from {Type} to {type} and it's not allowed");
            return this;
        }

        public GameFlowStateType GetBackState()
        {
            return GameFlowStateType.Lobby;
        } 
        
        public void StateEnter()
        {
            var Handle = _schedulerService.Schedule(() => _gameplayDataService.IsReady);
            Handle.OnScheduleTick += () =>
            {
                _eventService.Raise(new GameFlowStateEvent(GameFlowStateType.Gameplay));
                _gameplayLogic = new GameplayLogic(_eventService);
            };
        }
    }
}