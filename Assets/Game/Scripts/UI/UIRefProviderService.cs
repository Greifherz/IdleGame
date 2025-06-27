using Game.UI.Aggregators;
using Game.UI.View;
using ServiceLocator;

namespace Game.UI
{
    public class UIRefProviderService : IUIRefProviderService
    {
        public IMiningView MiningView { get; private set; }
        
        public void Initialize()
        {
            
        }

        //Signature asks for the mono context only for "bureaucracy", meaning this is a method only the GameplayStateMonoContext should be cleared to call
        public void SetMiningView(GameplayStateMonoContext holder, IMiningView miningView)
        {
            MiningView = miningView;
        }
    }
}