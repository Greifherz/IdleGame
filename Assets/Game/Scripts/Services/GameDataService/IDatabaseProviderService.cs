using Game.Gameplay;
using Game.Scripts.Army;
using ServiceLocator;

namespace Game.Scripts.Services.GameDataService
{
    public interface IDatabaseProviderService : IGameService
    {
        ArmyUnitDatabase ArmyUnitDatabase { get; }
        AnimationDatabase AnimationDatabase { get; }
        BattleDatabase BattleDatabase { get; }
    }
}