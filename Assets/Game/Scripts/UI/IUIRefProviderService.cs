using Game.UI.Aggregators;
using Game.UI.View;
using ServiceLocator;

namespace Game.UI
{
    public interface IUIRefProviderService : IGameService
    {
        IMiningView MiningView { get; }
        void SetMiningView(GameplayStateMonoContext holder, IMiningView miningView);
    }
}