namespace Game.Data.PersistentData
{
    public struct GameplayPersistentData
    {
        public PlayerPersistentData PlayerPersistentData { get; private set; }
        public EnemyPersistentData[] EnemyPersistentDatas { get; private set; }
        
        public GameplayPersistentData(PlayerPersistentData playerPersistentData, EnemyPersistentData[] enemyPersistentDatas)
        {
            PlayerPersistentData = playerPersistentData;
            EnemyPersistentDatas = enemyPersistentDatas;
        }

        public static GameplayPersistentData CreateDefaultPersistentData()
        {
            return new GameplayPersistentData(PersistentData.PlayerPersistentData.CreateDefaultPlayerData(),EnemyPersistentData.CreateDefaultEnemyData());
        }

        public EnemyPersistentData GetEnemyPersistentData(int enemyDataEnemyId)
        {
            if (enemyDataEnemyId >= EnemyPersistentDatas.Length)
                return new EnemyPersistentData();
            return EnemyPersistentDatas[enemyDataEnemyId];
        }
    }
}