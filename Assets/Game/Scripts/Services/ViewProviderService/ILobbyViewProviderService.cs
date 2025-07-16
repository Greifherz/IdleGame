using Services.ViewProvider.View;

namespace Services.ViewProvider
{
    public interface ILobbyViewProviderService : IViewProviderService
    {
        IMiningView MiningView { get; }
        IArmyView ArmyView { get; }
    }
}