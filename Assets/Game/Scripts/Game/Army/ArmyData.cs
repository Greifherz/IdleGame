using System;
using System.Collections.Generic;

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

        public static List<ArmyData> CreateDefaultArmiesData()
        {
            var values = Enum.GetValues(typeof(ArmyUnitType));
            var defaultData = new List<ArmyData>(values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                defaultData.Add(new ArmyData
                {
                    UnitType = (ArmyUnitType)values.GetValue(i),
                    Amount = 0
                });
            }

            return defaultData;
        }
    }
}