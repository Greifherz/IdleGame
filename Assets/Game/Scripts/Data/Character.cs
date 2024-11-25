namespace Game.Data
{
    public class Character : ICharacter
    {
        public string Name { get; private set; }
        public int HealthPoints { get; private set; }
        public int CurrentHealthPoints { get; private set; }
        public int ArmorPoints { get; private set; }
        public int AttackPoints { get; private set; }
        
        public float HealthPercentage => CurrentHealthPoints / HealthPoints;

        public Character(string name,int healthPoints, int armorPoints, int attackPoints)
        {
            Name = name;
            CurrentHealthPoints = HealthPoints = healthPoints;
            ArmorPoints = armorPoints;
            AttackPoints = attackPoints;
        }
    
        public Character(int healthPoints, int armorPoints, int attackPoints)
        {
            CurrentHealthPoints = HealthPoints = healthPoints;
            ArmorPoints = armorPoints;
            AttackPoints = attackPoints;
        }

        public void TakeDamage(int damage)
        {
            var damageToTake = damage - ArmorPoints;
            if (damageToTake < 0)
                damageToTake = 0;
        
            CurrentHealthPoints -= damageToTake;
        
            if (CurrentHealthPoints == 0)
            {
                Die();
            }
            else
            {
                //DamageTaken Event
            }
        }

        private void Die()
        {
            //Death event
        }
    }
}
