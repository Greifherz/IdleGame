using Services.ViewProvider.Aggregators;
using ServiceLocator;
using Services.ViewProvider.View;

namespace Services.ViewProvider
{
    public interface IViewProviderService : IGameService
    {
        IMiningView MiningView { get; }
        IArmyView ArmyView { get; }
        void SetMiningView(MinerStateMonoContext holder, IMiningView miningView);
        void SetArmyView(ArmyStateMonoContext holder, IArmyView armyView);
    }
}