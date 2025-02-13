﻿using Game.Data.GameplayData;
using Game.GameLogic;
using ServiceLocator;
using Services.EventService;
using Services.GameDataService;
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
        private IGamePersistenceDataService _gamePersistenceDataService;
        
        private IEventHandler _playerStatsEventHandler;

        private GameplayLogic _gameplayLogic;
        private PlayerStatsController _statsController;

        private bool _statsShown = false;

        public GameFlowGameplayState(IEventService eventService)
        {
            _eventService = eventService;
            _schedulerService = Locator.Current.Get<ISchedulerService>();
            _gameplayDataService = Locator.Current.Get<IGameplayDataService>();
            _gamePersistenceDataService = Locator.Current.Get<IGamePersistenceDataService>();
            
            _playerStatsEventHandler = new GameplayPlayerStatsVisibilityEventHandler(OnPlayerStatsVisibilityEvent);
            _eventService.RegisterListener(_playerStatsEventHandler,EventPipelineType.ViewPipeline);
            
            _gameplayDataService.Initialize();
        }

        private void OnPlayerStatsVisibilityEvent(IGameplayPlayerStatsVisibilityEvent visibilityEvent)
        {
            _statsShown = visibilityEvent.Visibility;
            if (_statsController == null)
            {
                _statsController = new PlayerStatsController();
            }

            _statsController.Display();
        }

        public bool CanTransitionTo(GameFlowStateType type)
        {
            return type == GameFlowStateType.Lobby;
        }

        public IGameFlowState TransitionTo(GameFlowStateType type)
        {
            if (CanTransitionTo(type))
            {
                _eventService.Raise(new GameplayDataPersistenceEvent(),EventPipelineType.ServicesPipeline);
                return new GameFlowLobbyState(_eventService);
            }

            if (type == GameFlowStateType.Gameplay && _statsShown)//TODO - try to remove this if
            {
                _statsShown = false;
                //Raise show/hide stats event
                _eventService.Raise(new GameplayPlayerStatsVisibilityEvent(false),EventPipelineType.ViewPipeline);
                return this;
            }
            
            Debug.LogError($"Tried to transition from {Type} to {type} and it's not allowed");
            return this;
        }

        public GameFlowStateType GetBackState()
        {
            if (_statsShown)
            {
                return GameFlowStateType.Gameplay;
            }
            return GameFlowStateType.Lobby;
        } 
        
        public void StateEnter()
        {
            var Handle = _schedulerService.Schedule(() => _gameplayDataService.IsReady);
            Handle.OnScheduleTick += () =>
            {
                _eventService.Raise(new GameFlowStateEvent(GameFlowStateType.Gameplay));
                _gameplayLogic ??= new GameplayLogic(_eventService);
            };
        }
    }
}