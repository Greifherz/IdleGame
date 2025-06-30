using System;

namespace Game.Scripts.Game
{
    [Serializable]
    public class MiningData
    {
        public DateTime LastCollectedTime;//TODO
        
        public int AcumulatedGold;
        public int ActiveMiners = 1;
        public int GoldPerMiner = 1;
    }
}