using System;
using UnityEngine;

namespace Game.Scripts.Army
{
    [Serializable]
    public class ArmyUnitData
    {
        public ArmyUnitType UnitType;
        public int Health;
        public int Attack;
        public int CostPerUnit;

        //TODO - Expand as the game needs more complexity.
    }
}
