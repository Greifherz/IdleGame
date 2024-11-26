namespace Game.Data
{
    public class PlayerCharacter : IPlayerCharacter
    {
        private ICharacter _characterImplementation;

        public string Name { get; private set; }
        public int HealthPoints => _characterImplementation.HealthPoints;

        public int CurrentHealthPoints => _characterImplementation.CurrentHealthPoints;

        public int ArmorPoints => _characterImplementation.ArmorPoints;

        public int AttackPoints => _characterImplementation.AttackPoints;
        public float HealthPercentage => _characterImplementation.HealthPercentage;

        public int Level { get; private set; }
        public int ExperiencePoints { get; private set; }
        
        public PlayerCharacter(ICharacter characterImplementation, int level, int experiencePoints)
        {
            _characterImplementation = characterImplementation;
            Name = characterImplementation.Name;
            Level = level;
            ExperiencePoints = experiencePoints;
        }
        
        public void TakeDamage(int damage)
        {
            _characterImplementation.TakeDamage(damage);
        }

        public void RestoreHealth()
        {
            _characterImplementation.RestoreHealth();
        }

        public void Die()
        {
            //PlayerDeath event
        }
    }
}