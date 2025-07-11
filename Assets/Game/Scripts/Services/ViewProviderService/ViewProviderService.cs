
using Services.ViewProvider.View;

namespace Services.ViewProvider
{
    public class ViewProviderService : IViewProviderService //Deprecated but here in case I want another layer of distance from Unity
    {
        public IMiningView MiningView { get; private set; }
        public IMultiArmyView MultiArmyView { get; private set; }

        public void Initialize()
        {
            
        }

        //Signature asks for the mono context only for "bureaucracy", meaning this is a method only the MinerStateMonoContext should be cleared to call
        public void SetMiningView(MinerStateMonoContext holder, IMiningView miningView)
        {
            MiningView = miningView;
        }

        // public void SetArmyView(ArmyStateMonoContext holder, IArmyView armyView)
        // {
        //     ArmyView = armyView;
        // }
    }
}