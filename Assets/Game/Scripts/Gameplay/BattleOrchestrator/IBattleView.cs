using System.Collections.Generic;
using Game.Gameplay;

namespace Game.Scripts.Gameplay.BattleOrchestrator
{
    public interface IBattleView
    {
        List<IUnitView> FriendlyUnits { get; }
        List<IUnitView> EnemyUnits { get; }
    }
}