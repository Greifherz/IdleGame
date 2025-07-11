using Game.Data.GameplayData;
using Game.Scripts.Mining;
using ServiceLocator;
using Services.EventService;
using Services.GameDataService;
using Services.Scheduler;
using UnityEngine;

namespace Game.GameFlow
{
    public class GameFlowMiningState : AbstractGameFlowState
    {
        public override GameFlowStateType Type => GameFlowStateType.Mining;
        private ISchedulerService _schedulerService;
        private IGameplayDataService _gameplayDataService;

        private bool _statsShown = false;
        private MiningPresenter _miningPresenter;

        public GameFlowMiningState (GameFlowStateFactory stateFactory,IEventService eventService) : base(stateFactory,eventService)
        {
            _schedulerService = Locator.Current.Get<ISchedulerService>();
            _gameplayDataService = Locator.Current.Get<IGameplayDataService>();
            
            _gameplayDataService.Initialize();
        }
        
        public override bool CanTransitionTo(GameFlowStateType type)
        {
            return type != Type && type != GameFlowStateType.Lobby;
        }

        public override void StateExit()
        {
            
        }

        public override GameFlowStateType GetBackState()
        {
            if (_statsShown)
            {
                return GameFlowStateType.Mining;
            }
            return GameFlowStateType.Lobby;
        } 
        
        public override void StateEnter()
        {
            if (_miningPresenter == null)
            {
                var Handle = _schedulerService.Schedule(() => _gameplayDataService.IsReady);
                Handle.OnScheduleTick += () =>
                {
                    _miningPresenter = new MiningPresenter(_gameplayDataService.GameplayData);
                    _eventService.Raise(new GameFlowStateEvent(GameFlowStateType.Mining));
                };
            }
            else
            {
                _eventService.Raise(new GameFlowStateEvent(GameFlowStateType.Mining));
            }
        }
    }
}