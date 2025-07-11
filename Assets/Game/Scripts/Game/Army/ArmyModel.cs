using System.Collections.Generic;
using Game.Data.GameplayData;
using Game.Scripts.Services.GameDataService;
using ServiceLocator;

namespace Game.Scripts.Army
{
    public class ArmyModel
    {
        private GameplayData _gameplayData;
        private List<ArmyData> _armiesData; //The armies the player actually have
        private ArmyUnitDatabase _armyUnitDatabase;

        public ArmyModel(GameplayData gameplayData)
        {
            var DatabaseProviderService = Locator.Current.Get<IDatabaseProviderService>();
            _armyUnitDatabase = DatabaseProviderService.ArmyUnitDatabase;
            
            _gameplayData = gameplayData;
            _armiesData = gameplayData.ArmyDatas;
        }
        
        public int Hire(ArmyUnitType armyUnitType)
        {
            var UnitData = _armyUnitDatabase.Get(armyUnitType);
            _gameplayData.OverallGold -= UnitData.CostPerUnit;
            bool found = false;
            foreach (var ArmyData in _armiesData)
            {
                if (ArmyData.UnitType != armyUnitType) continue;
                ArmyData.Amount++;
                found = true;
                break;
            }

            if (!found)
            {
                var NewArmy = new ArmyData();
                NewArmy.UnitType = armyUnitType;
                NewArmy.Amount = 1;
                _armiesData.Add(NewArmy);
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

        public ArmyUnitData GetArmyUnitData(ArmyUnitType armyUnitType)
        {
            return _armyUnitDatabase.Get(armyUnitType);
        }
    }
}