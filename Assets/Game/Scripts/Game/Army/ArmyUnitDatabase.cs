using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Army
{
    [CreateAssetMenu(fileName = "ArmyUnitDatabase", menuName = "ScriptableObjects/ArmyUnitDatabase", order = 1)]
    public class ArmyUnitDatabase : ScriptableObject
    {
        [SerializeField] private List<ArmyUnitData> Database; //Id will be an index for now, but in the future it will be best if it was a proper database
        //Also in the future, far future, this is data that would come from a remote config.

        public ArmyUnitData Get(ArmyUnitType type)
        {
            return Database[(int)type];
        }
    }
}