using Game.Data.GameplayData;
using Game.Gameplay;
using ServiceLocator;
using Services.EventService;
using Services.Scheduler;

namespace Game.GameFlow
{
    public class GameFlowBattleState : AbstractGameFlowState
    {
        public override GameFlowStateType Type => GameFlowStateType.Battle;

        private ISchedulerService _schedulerService;
        private IGameplayDataService _gameplayDataService;

        private BattleOrchestrator _battleOrchestrator;
        
        public GameFlowBattleState(GameFlowStateFactory gameFlowStateFactory, IEventService eventService) : base(gameFlowStateFactory, eventService)
        {
            _schedulerService = Locator.Current.Get<ISchedulerService>();
            _gameplayDataService = Locator.Current.Get<GameplayDataService>();
        }

        public override void StateEnter()
        {
            
        }

        public override bool CanTransitionTo(GameFlowStateType type)
        {
            return type != Type && type != GameFlowStateType.Start;
        }

        public override GameFlowStateType GetBackState()
        {
            return GameFlowStateType.ArmyView;
        }

        public override void StateExit()
        {
            
        }
    }
}