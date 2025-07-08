using System;
using Game.Data.GameplayData;

namespace Game.Scripts.Mining
{
    /// <summary>
    /// The "Model" in MVP.
    /// Holds all data and pure business logic for the mining feature.
    /// This class is the single source of truth for the mining state.
    /// It knows nothing about Unity, UI, or services.
    /// </summary>
    public class MiningModel
    {
        private GameplayData _gameplayData;
        private MiningData _miningData;
        
        public int ActiveMiners => _miningData.ActiveMiners;
        public int AcumulatedGold => _miningData.AcumulatedGold;
        
        public int CurrentHireCost => 45 + _miningData.ActiveMiners * 5;

        private DateTime LastCollectedTime => _miningData.LastCollectedTime;

        public void AddAccumulatedGold()
        {
            _miningData.AcumulatedGold += _miningData.ActiveMiners * _miningData.GoldPerMiner;
        }

        public bool CanAffordToHire()
        {
            // For now, we'll assume we are spending from the accumulated pool.
            return _gameplayData.OverallGold >= CurrentHireCost;
        }

        public void HireMiner()
        {
            if (!CanAffordToHire()) return;

            _gameplayData.OverallGold -= CurrentHireCost;
            _miningData.ActiveMiners++;
        }

        public int CollectAllGold()
        {
            int collectedAmount = _miningData.AcumulatedGold;
            _miningData.AcumulatedGold = 0;
            _miningData.LastCollectedTime = DateTime.UtcNow;
            return collectedAmount;
        }

        public void LoadFrom(GameplayData persistentData)
        {
            _gameplayData = persistentData;
            _miningData = _gameplayData.MiningData;
            
            // You would also calculate offline progress here and add it to AccumulatedGold
            var OfflineAccumulation = (DateTime.UtcNow - LastCollectedTime).TotalSeconds / MiningPresenter.TICK_TIME * _miningData.ActiveMiners * _miningData.GoldPerMiner;
            _miningData.AcumulatedGold += (int)Math.Floor(OfflineAccumulation);
        }

        public MiningData ToData()
        {
            return _miningData;
        }
    }
    
}