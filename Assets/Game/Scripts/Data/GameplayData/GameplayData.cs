using System;
using System.Collections.Generic;
using Game.Scripts.Army;
using Game.Scripts.Mining;

namespace Game.Data.GameplayData
{
    [Serializable]
    public class GameplayData
    {
        public int OverallGold = 0;
        public MiningData MiningData;//TODO - do something about this being public to set
        public List<ArmyData> ArmyDatas;
        
        //If needed turn this into a factory or something
        public static GameplayData CreateDefaultGameplayData()
        {
            return new GameplayData
            {
                MiningData = MiningData.CreateDefaultMiningData(),
                ArmyDatas = ArmyData.CreateDefaultArmiesData()
            };
        }
    }
}