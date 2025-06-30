using System;

using Game.Scripts.Game;

namespace Game.Data.GameplayData
{
    [Serializable]
    public class GameplayData
    {
        public MiningData MiningData { get; private set; }
        
        //If needed turn this into a factory or something
        public static GameplayData CreateDefaultGameplayData()
        {
            var data = new GameplayData();
            
            data.MiningData = new MiningData();
            
            return data;
        }
        
        
        
    }
}