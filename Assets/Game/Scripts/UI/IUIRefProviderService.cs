using Game.UI.Aggregators;
using ServiceLocator;

namespace Game.UI
{
    public interface IUIRefProviderService : IGameService
    {
        StatsAggregatorContext StatsAggregatorContext { get; }
        void SetStatsAggregator(GameplayStateMonoContext holder, StatsAggregatorContext statsAggregator);
    }
}