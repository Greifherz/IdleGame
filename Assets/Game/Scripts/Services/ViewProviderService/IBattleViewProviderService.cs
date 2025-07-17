using Game.Gameplay;

namespace Services.ViewProvider
{
    public interface IBattleViewProviderService : IViewProviderService
    {
        IBattleView BattleView { get; }
    }
}