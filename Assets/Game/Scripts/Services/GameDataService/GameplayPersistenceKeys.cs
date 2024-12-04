namespace Services.GameDataService
{
    public static class GameplayPersistenceKeys
    {
        public const string PERSISTENCE_COUNT_MARKUP = "-&-";
        
        public const string GAMEPLAY_DATA_KEY = "GameplayDataKey01";
        public const string GAMEPLAY_DATA_PLAYER_NAME_MOD = "Name";
        public const string GAMEPLAY_DATA_PLAYER_LEVEL_MOD = "Level";
        public const string GAMEPLAY_DATA_PLAYER_XP_MOD = "Experience";
        public const string GAMEPLAY_DATA_PLAYER_HP_MOD = "HealthPoints";
        public const string GAMEPLAY_DATA_PLAYER_CURRENTHP_MOD = "CurrentHealthPoints";
        public const string GAMEPLAY_DATA_PLAYER_ATK_MOD = "Attack";
        public const string GAMEPLAY_DATA_PLAYER_ARMOR_MOD = "Armor";
        public const string GAMEPLAY_DATA_PLAYER_DEATH_MOD = "Deaths";
        
        public const string GAMEPLAY_DATA_ENEMY_COUNT_MOD = "EnemyCount";
        public const string GAMEPLAY_DATA_ENEMY_ID_MOD = PERSISTENCE_COUNT_MARKUP + "ID"; //Use interpolation instead of this markup
        public const string GAMEPLAY_DATA_ENEMY_HP_MOD = PERSISTENCE_COUNT_MARKUP + "HealthPoints"; //Use interpolation instead of this markup
        public const string GAMEPLAY_DATA_ENEMY_KILLCOUNT_MOD = PERSISTENCE_COUNT_MARKUP + "KillCount"; //Use interpolation instead of this markup
    }
}