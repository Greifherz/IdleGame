using Game.UI.Aggregators;
using ServiceLocator;

namespace Game.UI
{
    public class UIRefProviderService : IUIRefProviderService
    {
        public GameplayAggregatorContext GameplayAggregatorContext { get; private set; }
        
        public void Initialize()
        {
            
        }

        //Signature asks for the mono context only for "bureaucracy", meaning this is a method only the GameplayStateMonoContext should be cleared to call
        public void SetGameplayAggregator(GameplayStateMonoContext holder, GameplayAggregatorContext aggregator)
        {
            GameplayAggregatorContext = aggregator;
        }
    }
}