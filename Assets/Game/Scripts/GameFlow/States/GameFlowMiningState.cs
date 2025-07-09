using Game.Data.GameplayData;
using ServiceLocator;
using Services.EventService;
using Services.GameDataService;
using Services.Scheduler;
using UnityEngine;

namespace Game.GameFlow
{
    public class GameFlowMiningState : IGameFlowState
    {
        public GameFlowStateType Type => GameFlowStateType.Mining;
        private IEventService _eventService;
        private ISchedulerService _schedulerService;
        private IGameplayDataService _gameplayDataService;
        


        private bool _statsShown = false;

        public GameFlowMiningState(IEventService eventService)
        {
            _eventService = eventService;
            _schedulerService = Locator.Current.Get<ISchedulerService>();
            _gameplayDataService = Locator.Current.Get<IGameplayDataService>();
            
            _gameplayDataService.Initialize();
        }
        
        public bool CanTransitionTo(GameFlowStateType type)
        {
            return type != Type && type != GameFlowStateType.Lobby;
        }

        public IGameFlowState TransitionTo(GameFlowStateType type)
        {
            if (CanTransitionTo(type))
            {
                _eventService.Raise(new GameplayDataPersistenceEvent(),EventPipelineType.ServicesPipeline);
                return new GameFlowLobbyState(_eventService);
            }

            Debug.LogError($"Tried to transition from {Type} to {type} and it's not allowed");
            return this;
        }

        public GameFlowStateType GetBackState()
        {
            if (_statsShown)
            {
                return GameFlowStateType.Mining;
            }
            return GameFlowStateType.Lobby;
        } 
        
        public void StateEnter()
        {
            var Handle = _schedulerService.Schedule(() => _gameplayDataService.IsReady);
            Handle.OnScheduleTick += () =>
            {
                _eventService.Raise(new GameFlowStateEvent(GameFlowStateType.Mining));
            };
        }
    }
}