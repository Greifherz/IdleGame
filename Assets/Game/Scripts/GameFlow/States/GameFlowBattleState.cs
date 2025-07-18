using Game.Data.GameplayData;
using Game.Gameplay;
using Game.Scripts.Services.GameDataService;
using ServiceLocator;
using Services.EventService;
using Services.Scheduler;
using Services.TickService;
using Services.ViewProvider;

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
            var BattleViewProviderService = Locator.Current.Get<IBattleViewProviderService>();
            var DatabaseProviderService = Locator.Current.Get<IDatabaseProviderService>();
            var TickService = Locator.Current.Get<ITickService>();
            var SceneStateDataService = Locator.Current.Get<ISceneDataService>();

            var BattleStateData = SceneStateDataService.GetData<BattleStateData>();
            
            _battleOrchestrator = new BattleOrchestrator(BattleViewProviderService.BattleView,_gameplayDataService,DatabaseProviderService,TickService,BattleStateData);
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
            _battleOrchestrator.Dispose();
        }
    }
}