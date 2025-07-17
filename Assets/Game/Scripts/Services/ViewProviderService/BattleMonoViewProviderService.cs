using System;
using Game.Gameplay;
using ServiceLocator;
using UnityEngine;

namespace Services.ViewProvider
{
    public class BattleMonoViewProviderService : MonoBehaviour, IBattleViewProviderService
    {
        [SerializeField] private BattleMonoContext _battleMonoContext;
        
        public IBattleView BattleView => _battleMonoContext.GetBattleAggregatorContext();
        
        public void Initialize()
        {
            //For now, do nothing
        }

        private void Awake()
        {
            Locator.Current.Register<IBattleViewProviderService>(this);
        }

        private void OnDestroy()
        {
            Locator.Current.Unregister<IBattleViewProviderService>();
        }
    }
}