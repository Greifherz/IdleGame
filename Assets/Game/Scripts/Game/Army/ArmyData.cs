using System;

namespace Game.Scripts.Army
{
    [Serializable]
    public class ArmyData
    {
        public ArmyUnitType UnitType;
        public int Amount;
        
        public static ArmyData CreateDefaultArmyData()
        {
            return new ArmyData
            {
                UnitType = ArmyUnitType.Soldier,
                Amount = 0
            };
        }
    }
}