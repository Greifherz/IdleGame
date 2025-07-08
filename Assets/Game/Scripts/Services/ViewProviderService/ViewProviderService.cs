using Services.ViewProvider.Aggregators;
using ServiceLocator;
using Services.ViewProvider.View;

namespace Services.ViewProvider
{
    public class ViewProviderService : IViewProviderService
    {
        public IMiningView MiningView { get; private set; }
        public IArmyView ArmyView { get; private set; }

        public void Initialize()
        {
            
        }

        //Signature asks for the mono context only for "bureaucracy", meaning this is a method only the MinerStateMonoContext should be cleared to call
        public void SetMiningView(MinerStateMonoContext holder, IMiningView miningView)
        {
            MiningView = miningView;
        }

        public void SetArmyView(ArmyStateMonoContext holder, IArmyView armyView)
        {
            ArmyView = armyView;
        }
    }
}