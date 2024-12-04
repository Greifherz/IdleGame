using System;
using Game.Data.PersistentData;
using UnityEngine;

namespace Game.Data.GameplayData
{
    [Serializable]
    public struct EnemyData
    {//TODO - separate this data from persisted data
        [HideInInspector] public int EnemyId;  //The ID is the index on this database
        public string EnemyName;
        public int HealthPoints;
        public int AttackPoints;
        public int ArmorPoints;
        public int XpReward;
        
        public EnemyPersistentData PersistentData;

        public IEnemyCharacter ToEnemyCharacter(Action<IEnemyCharacter> onDeath = null)
        {
            return new EnemyCharacter(EnemyId,EnemyName,XpReward,HealthPoints,ArmorPoints,AttackPoints, onDeath);
        }
    }
}