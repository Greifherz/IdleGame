using System.Collections.Generic;

namespace Game.Gameplay
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