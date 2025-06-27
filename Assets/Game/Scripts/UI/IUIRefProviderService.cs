using Game.UI.Aggregators;
using ServiceLocator;

namespace Game.UI
{
    public interface IUIRefProviderService : IGameService
    {
        GameplayAggregatorContext GameplayAggregatorContext { get; }
        void SetGameplayAggregator(GameplayStateMonoContext holder, GameplayAggregatorContext aggregator);
    }
}