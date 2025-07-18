using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(fileName = "BattleDatabase", menuName = "ScriptableObjects/BattleDatabase", order = 1)]
    public class BattleDatabase : ScriptableObject
    {
        [SerializeField] private BattleData[] Battles; //For now id = index

        public BattleData GetBattleData(int id)
        {
            return Battles[id];
        }
    }
}