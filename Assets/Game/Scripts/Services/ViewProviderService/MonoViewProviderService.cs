using ServiceLocator;
using Services.ViewProvider.View;
using UnityEngine;

namespace Services.ViewProvider
{
    public class MonoViewProviderService : MonoBehaviour, IViewProviderService
    {
        [SerializeField] private MinerStateMonoContext _minerStateContext;
        [SerializeField] private ArmyStateMonoContext _armyStateContext;

        public IMiningView MiningView => _minerStateContext.CreateMiningView();
        public IMultiArmyView MultiArmyView => _armyStateContext.CreateArmyMultiView();

        public void Initialize()
        {
            //For now, do nothing
        }

        private void Awake()
        {
            Locator.Current.Register<IViewProviderService>(this);
        }
    }
}