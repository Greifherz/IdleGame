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
        public int ActiveMiners { get; private set; } = 1;
        public int AccumulatedGold { get; private set; } = 0;
        public int GoldPerMiner { get; private set; } = 1;
        public int CurrentHireCost => 45 + ActiveMiners * 5;

        public void AddAccumulatedGold()
        {
            AccumulatedGold += ActiveMiners * GoldPerMiner;
        }

        public bool CanAffordToHire()
        {
            // For now, we'll assume we are spending from the accumulated pool.
            return AccumulatedGold >= CurrentHireCost;
        }

        public void HireMiner()
        {
            if (!CanAffordToHire()) return;

            AccumulatedGold -= CurrentHireCost;
            ActiveMiners++;
        }

        public int CollectAllGold()
        {
            int collectedAmount = AccumulatedGold;
            AccumulatedGold = 0;
            return collectedAmount;
        }

        public void LoadFrom(MiningData persistentData)
        {
            this.ActiveMiners = persistentData.ActiveMiners;
            
            // You would also calculate offline progress here and add it to AccumulatedGold
        }
    }
    
}