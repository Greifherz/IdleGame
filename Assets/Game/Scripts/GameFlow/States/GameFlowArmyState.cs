using Game.Data.GameplayData;
using Game.Scripts.Army;
using ServiceLocator;
using Services.EventService;
using Services.GameDataService;
using Services.Scheduler;
using UnityEngine;

namespace Game.GameFlow
{
    public class GameFlowArmyState : AbstractGameFlowState
    {
        public override GameFlowStateType Type => GameFlowStateType.ArmyView;

        private ISchedulerService _schedulerService;
        private IGameplayDataService _gameplayDataService;

        private ArmyPresenter _armyPresenter;
        
        public GameFlowArmyState(IEventService eventService) : base(eventService)
        {
            _schedulerService = Locator.Current.Get<ISchedulerService>();
            _gameplayDataService = Locator.Current.Get<IGameplayDataService>();
        }

        public override void StateEnter()
        {
            var Handle = _schedulerService.Schedule(() => _gameplayDataService.IsReady);
            Handle.OnScheduleTick += () =>
            {
                _armyPresenter = new ArmyPresenter(_gameplayDataService.GameplayData);
                _eventService.Raise(new GameFlowStateEvent(GameFlowStateType.ArmyView));
            };
        }

        protected override void StateExit()
        {
            _armyPresenter = null;
        }

        public override GameFlowStateType GetBackState()
        {
            return GameFlowStateType.Mining;
        } 

        public override bool CanTransitionTo(GameFlowStateType type)
        {
            return type != Type && type != GameFlowStateType.Lobby;
        }
    }
}