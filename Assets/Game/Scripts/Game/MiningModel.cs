using System;

namespace Game.Scripts.Game
{
    /// <summary>
    /// The "Model" in MVP.
    /// Holds all data and pure business logic for the mining feature.
    /// This class is the single source of truth for the mining state.
    /// It knows nothing about Unity, UI, or services.
    /// </summary>
    public class MiningModel
    {
        public MiningData MiningData;
        public int ActiveMiners => MiningData.ActiveMiners;
        public int AcumulatedGold => MiningData.AcumulatedGold;
        public int GoldPerMiner => MiningData.GoldPerMiner;
        
        public int CurrentHireCost => 45 + MiningData.ActiveMiners * 5;

        public DateTime LastCollectedTime => MiningData.LastCollectedTime;

        public void AddAccumulatedGold()
        {
            MiningData.AcumulatedGold += MiningData.ActiveMiners * MiningData.GoldPerMiner;
        }

        public bool CanAffordToHire()
        {
            // For now, we'll assume we are spending from the accumulated pool.
            return MiningData.AcumulatedGold >= CurrentHireCost;
        }

        public void HireMiner()
        {
            if (!CanAffordToHire()) return;

            MiningData.AcumulatedGold -= CurrentHireCost;
            MiningData.ActiveMiners++;
        }

        public int CollectAllGold()
        {
            int collectedAmount = MiningData.AcumulatedGold;
            MiningData.AcumulatedGold = 0;
            MiningData.LastCollectedTime = DateTime.UtcNow;
            return collectedAmount;
        }

        public void LoadFrom(MiningData persistentData)
        {
            MiningData = persistentData;
            
            // You would also calculate offline progress here and add it to AccumulatedGold
            var OfflineAccumulation = (DateTime.UtcNow - LastCollectedTime).TotalSeconds / MiningPresenter.TICK_TIME * MiningData.ActiveMiners * MiningData.GoldPerMiner;
            MiningData.AcumulatedGold += (int)Math.Floor(OfflineAccumulation);
        }

        public MiningData ToData()
        {
            return MiningData;
        }
    }
    
}