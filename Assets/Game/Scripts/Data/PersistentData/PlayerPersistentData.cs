namespace Game.Data.PersistentData
{
    public struct PlayerPersistentData
    {
        public string Name { get; private set; }
        
        public int HealthPoints { get; private set; }
        public int CurrentHealthPoints { get; private set; }
        public int ArmorPoints { get; private set; }
        public int AttackPoints { get; private set; }
        
        public int Level { get; private set; }
        public int ExperiencePoints { get; private set; }
        
        public int DeathCount { get; private set; }
        
        public PlayerPersistentData(string name, int healthPoints, int currentHealthPoints, int armorPoints, int attackPoints, int level, int experiencePoints, int deathCount)
        {
            Name = name;
            HealthPoints = healthPoints;
            CurrentHealthPoints = currentHealthPoints;
            ArmorPoints = armorPoints;
            AttackPoints = attackPoints;
            Level = level;
            ExperiencePoints = experiencePoints;
            DeathCount = deathCount;
        }
    }
}