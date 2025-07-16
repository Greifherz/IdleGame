using System;
using ServiceLocator;
using Services.ViewProvider.View;
using UnityEngine;

namespace Services.ViewProvider
{
    public class LobbyMonoViewProviderService : MonoBehaviour, ILobbyViewProviderService
    {
        [SerializeField] private MinerStateMonoContext _minerStateContext;
        [SerializeField] private ArmyStateMonoContext _armyStateContext;

        public IMiningView MiningView => _minerStateContext.CreateMiningView();
        public IArmyView ArmyView => _armyStateContext.CreateArmyView();

        public void Initialize()
        {
            //For now, do nothing
        }

        private void Awake()
        {
            Locator.Current.Register<ILobbyViewProviderService>(this);
        }

        private void OnDestroy()
        {
            Locator.Current.Unregister<ILobbyViewProviderService>();
        }
    }
}