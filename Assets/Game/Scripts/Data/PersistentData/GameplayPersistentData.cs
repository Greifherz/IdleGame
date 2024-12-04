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
    }
}