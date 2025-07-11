using Game.Scripts.Army;
using ServiceLocator;

namespace Game.Scripts.Services.GameDataService
{
    public interface IDatabaseProviderService : IGameService
    {
        ArmyUnitDatabase ArmyUnitDatabase { get; }
    }
}