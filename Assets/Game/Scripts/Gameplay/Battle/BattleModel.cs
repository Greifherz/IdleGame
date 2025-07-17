using System.Collections.Generic;
using System.Linq;

namespace Game.Gameplay
{
    public class BattleModel
    {
        public List<UnitRuntimeData> FriendlyUnits { get; }
        public List<UnitRuntimeData> EnemyUnits { get; }

        public BattleModel()
        {
            FriendlyUnits = new List<UnitRuntimeData>();
            EnemyUnits = new List<UnitRuntimeData>();
        }
    }
}