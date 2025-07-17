using System.Collections.Generic;
using Game.Gameplay;

namespace Game.Gameplay
{
    public interface IBattleView
    {
        List<IUnitView> FriendlyUnits { get; }
        List<IUnitView> EnemyUnits { get; }
    }
}