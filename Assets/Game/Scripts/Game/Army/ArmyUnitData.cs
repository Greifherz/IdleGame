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
        public int MoveSpeed;
        public float AtkSpeed;

        //TODO - Expand as the game needs more complexity.
    }
}
