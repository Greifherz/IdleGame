using System.Collections.Generic;
using Game.Gameplay;

namespace Game.Scripts.Gameplay.BattleOrchestrator
{
    public class BattleAggregatorContext : IBattleView
    {
        public List<IUnitView> FriendlyUnits { get; private set; }
        public List<IUnitView> EnemyUnits { get; private set; }
        //BackgroundSTuff

        public BattleAggregatorContext(UnitMonoContext[] friendlyUnitsMonoContext,UnitMonoContext[] enemyUnitsMonoContext)
        {
            FriendlyUnits = new List<IUnitView>();
            foreach (var Unit in friendlyUnitsMonoContext)
            {
                FriendlyUnits.Add(Unit.GetAggregatorContext());
            }
            
            EnemyUnits = new List<IUnitView>();
            foreach (var Unit in enemyUnitsMonoContext)
            {
                EnemyUnits.Add(Unit.GetAggregatorContext());
            }
        }
    }
}