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

        public DateTime LastCollectedTime = DateTime.UtcNow;

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
            LastCollectedTime = DateTime.UtcNow;
            return collectedAmount;
        }

        public void LoadFrom(MiningData persistentData)
        {
            this.ActiveMiners = persistentData.ActiveMiners;
            this.AccumulatedGold = persistentData.AcumulatedGold;
            this.GoldPerMiner = persistentData.GoldPerMiner;
            this.LastCollectedTime = persistentData.LastCollectedTime;
            
            // You would also calculate offline progress here and add it to AccumulatedGold
            var OfflineAccumulation = (DateTime.UtcNow - LastCollectedTime).TotalSeconds / MiningPresenter.TICK_TIME * ActiveMiners * GoldPerMiner;
            AccumulatedGold += (int)Math.Floor(OfflineAccumulation);
        }

        public MiningData ToData()
        {
            var Data = new MiningData
            {
                ActiveMiners = ActiveMiners,
                AcumulatedGold = AccumulatedGold,
                GoldPerMiner = GoldPerMiner,
                LastCollectedTime = LastCollectedTime
            };

            return Data;
        }
    }
    
}