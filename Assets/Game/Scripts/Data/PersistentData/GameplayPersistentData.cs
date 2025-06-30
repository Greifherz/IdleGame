namespace Game.Data.PersistentData
{
    public struct GameplayPersistentData //Maybe won't be used. But if I need to convert types into serializable or something, it's here!
    {
        public static GameplayPersistentData CreateDefaultPersistentData()
        {
            return new GameplayPersistentData();
        }
    }
}