using System;
using System.Collections.Generic;
using Game.Data.GameplayData;
using Game.Scripts.Army;
using Game.Scripts.Services.GameDataService;
using Services.TickService;

namespace Game.Gameplay
{
    public delegate List<IUnitView> TargetProviderDelegate();
    public class BattleOrchestrator : IDisposable
    {
        
        // --- Dependencies (Injected) ---
        private readonly IBattleView _view;
        private readonly IGameplayDataService _gameplayDataService;
        private readonly IDatabaseProviderService _databaseProvider;
        private readonly ITickService _tickService;

        private readonly int _battleId;

        // --- Owned Objects ---
        private List<UnitBehavior> _friendlyBehaviors;
        private List<UnitBehavior> _enemyBehaviors;
        
        public BattleOrchestrator(
            IBattleView view, 
            IGameplayDataService gameplayDataService, 
            IDatabaseProviderService databaseProvider, 
            ITickService tickService,
            BattleStateData battleStateData)
        {
            _view = view;
            _battleId = battleStateData.BattleId;
            _gameplayDataService = gameplayDataService;
            _databaseProvider = databaseProvider;
            _tickService = tickService;
        }

        // The setup logic is moved to a dedicated method.
        public void StartBattle()
        {
            var UnitDatabase = _databaseProvider.ArmyUnitDatabase;
            var AnimationDatabase = _databaseProvider.AnimationDatabase;
            var BattleDatabase = _databaseProvider.BattleDatabase;
            
            var ArmyData = _gameplayDataService.GameplayData.ArmyDatas;
            var BattleData = BattleDatabase.GetBattleData(_battleId);
            var EnemyArmyData = BattleData.Armies;

            // Get the lists of unit views from the IBattleView.
            var EnemyUnitViews = _view.EnemyUnits;
            var FriendlyUnitViews = _view.FriendlyUnits;
            
            TargetProviderDelegate friendlyProvider = () => _view.FriendlyUnits;
            TargetProviderDelegate enemyProvider = () => _view.EnemyUnits;

            _friendlyBehaviors = new List<UnitBehavior>(FriendlyUnitViews.Count);
            
            // Loop through the friendly units and create a behavior for each one.
            for (var I = 0; I < FriendlyUnitViews.Count; I++)
            {
                var UnitView = FriendlyUnitViews[I];
                if (I < ArmyData.Count)
                {
                    var UnitArmyData = ArmyData[I];
                    var UnitStaticData = UnitDatabase.Get(UnitArmyData.UnitType);
                
                    // Pass all necessary dependencies to the UnitBehavior.
                    var Behavior = new UnitBehavior(
                        _tickService,
                        AnimationDatabase,
                        UnitStaticData,
                        UnitArmyData.Amount,
                        UnitView,
                        enemyProvider
                    );
                
                    _friendlyBehaviors.Add(Behavior);
                }
                else
                {
                    UnitView.Dismiss();
                }
            }
            
            for (var I = 0; I < EnemyUnitViews.Count; I++)
            {
                var UnitView = EnemyUnitViews[I];
                if (I < EnemyArmyData.Count)
                {
                    var UnitArmyData = EnemyArmyData[I];
                    var UnitStaticData = UnitDatabase.Get(UnitArmyData.UnitType);

                    // Pass all necessary dependencies to the UnitBehavior.
                    var Behavior = new UnitBehavior(
                        _tickService,
                        AnimationDatabase,
                        UnitStaticData,
                        UnitArmyData.Amount,
                        UnitView,
                        friendlyProvider
                    );

                    _enemyBehaviors.Add(Behavior);
                }
                else
                {
                    UnitView.Dismiss();
                }
            }
        }

        // A cleanup method to be called by the GameFlow state.
        public void Dispose()
        {
            foreach (var Behavior in _friendlyBehaviors)
            {
                Behavior.Dispose();
            }
            _friendlyBehaviors.Clear();

            foreach (var Behavior in _enemyBehaviors)
            {
                Behavior.Dispose();
            }
            _enemyBehaviors.Clear();
        }
    }
}