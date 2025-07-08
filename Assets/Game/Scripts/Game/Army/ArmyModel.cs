using System.Collections.Generic;
using Game.Data.GameplayData;

namespace Game.Scripts.Army
{
    public class ArmyModel
    {
        private GameplayData _gameplayData;
        private List<ArmyData> _armiesData; //The armies the player actually have
        private ArmyUnitDatabase _armyUnitDatabase;

        public ArmyModel(GameplayData gameplayData, ArmyUnitDatabase database)
        {
            _gameplayData = gameplayData;
            _armyUnitDatabase = database;
        }
        
        public int Hire(ArmyUnitType type)
        {
            var UnitData = _armyUnitDatabase.Get(type);
            _gameplayData.OverallGold -= UnitData.CostPerUnit;
            foreach (var ArmyData in _armiesData)
            {
                if (ArmyData.UnitType != type) continue;
                ArmyData.Amount++;
                break;
            }

            return UnitData.CostPerUnit;
        }

        public bool CanHire(ArmyUnitType type)//Army Unit costs are flat, review in the future
        {
            var UnitData = _armyUnitDatabase.Get(type);
            return _gameplayData.OverallGold >= UnitData.CostPerUnit;
        }

        public ArmyData GetArmyDataOfIndex(int index)
        {
            if (index < 0 || index >= _armiesData.Count) return null;
            return _armiesData[index];
        }
    }
}