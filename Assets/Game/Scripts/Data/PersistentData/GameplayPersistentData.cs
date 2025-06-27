namespace Game.Data.PersistentData
{
    public struct GameplayPersistentData
    {
        public PlayerPersistentData PlayerPersistentData { get; private set; }
        
        public GameplayPersistentData(PlayerPersistentData playerPersistentData)
        {
            PlayerPersistentData = playerPersistentData;
        }

        public static GameplayPersistentData CreateDefaultPersistentData()
        {
            return new GameplayPersistentData(PersistentData.PlayerPersistentData.CreateDefaultPlayerData());
        }
    }
}