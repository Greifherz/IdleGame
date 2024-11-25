namespace Game.Data
{
    public class PlayerCharacter : ICharacter
    {
        private ICharacter _characterImplementation;
        public string Name { get; private set; }
        public int HealthPoints => _characterImplementation.HealthPoints;

        public int CurrentHealthPoints => _characterImplementation.CurrentHealthPoints;

        public int ArmorPoints => _characterImplementation.ArmorPoints;

        public int AttackPoints => _characterImplementation.AttackPoints;
        public float HealthPercentage => _characterImplementation.HealthPercentage;

        public PlayerCharacter(int healthPoints, int armorPoints, int attackPoints)
        {
            _characterImplementation = new Character(healthPoints,armorPoints,attackPoints);
        }
        
        public void TakeDamage(int damage)
        {
            _characterImplementation.TakeDamage(damage);
        }
    }
}