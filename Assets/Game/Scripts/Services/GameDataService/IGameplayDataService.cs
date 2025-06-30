using Game.Scripts.Game;
using ServiceLocator;

namespace Game.Data.GameplayData
{
    public interface IGameplayDataService : IGameService
    {
        bool IsReady { get; }
        GameplayData GameplayData { get; }
    }
}