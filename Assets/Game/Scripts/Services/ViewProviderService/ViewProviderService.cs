using Services.ViewProvider.Aggregators;
using ServiceLocator;
using Services.ViewProvider.View;

namespace Services.ViewProvider
{
    public class ViewProviderService : IViewProviderService
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