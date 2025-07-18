using System;
using System.Collections.Generic;
using Game.Scripts.Army;

namespace Game.Gameplay
{
    //Class to hold data related to battle. Mainly used for description of enemies but can be extended if modifiers will apply in the future
    [Serializable]
    public class BattleData
    {
        public List<ArmyData> Armies;
    }
}