using Game.Data.GameplayData;
using UnityEngine;

namespace Game.Scripts.Data
{
    //Don't like these laying around
    // [CreateAssetMenu(fileName = "GameEnemyDatabase", menuName = "ScriptableObjects/GameEnemyDatabase", order = 1)]
    public class GameEnemyDatabase : ScriptableObject
    {
        public EnemyData[] Enemies;
    }
}