namespace Game.Data.PersistentData
{
    public struct EnemyPersistentData
    {
        public int EnemyId { get; private set; }  //The ID is the index on this database
        public int KillCount { get; private set; }
        public int CurrentHealthPoints { get; private set; }

        public EnemyPersistentData(int enemyId, int killCount, int currentHealthPoints)
        {
            EnemyId = enemyId;
            KillCount = killCount;
            CurrentHealthPoints = currentHealthPoints;
        }
    }
}