using Game.UI.Aggregators;
using ServiceLocator;

namespace Game.UI
{
    public class UIRefProviderService : IUIRefProviderService
    {
        public StatsAggregatorContext StatsAggregatorContext { get; private set; }
        
        public void Initialize()
        {
            
        }

        //Signature asks for the mono context only for "bureaucracy", meaning this is a method only the GameplayStateMonoContext should be cleared to call
        public void SetStatsAggregator(GameplayStateMonoContext holder, StatsAggregatorContext statsAggregator)
        {
            StatsAggregatorContext = statsAggregator;
        }
    }
}