using System;

namespace Game.Data
{
    public class EnemyCharacter : IEnemyCharacter
    {
        private ICharacter _characterImplementation;
        private IEnemyCharacter _enemyCharacterImplementation;
        public string Name => _characterImplementation.Name;

        public int HealthPoints => _characterImplementation.HealthPoints;

        public int CurrentHealthPoints => _characterImplementation.CurrentHealthPoints;

        public int ArmorPoints => _characterImplementation.ArmorPoints;

        public int AttackPoints => _characterImplementation.AttackPoints;
        public float HealthPercentage => _characterImplementation.HealthPercentage;

        public int Id { get; }
        public int KillCount { get; private set; }
        public int XpReward { get; private set; }
        
        public EnemyCharacter(int id,string name,int xpReward,int healthPoints,int currentHealthPoints, int armorPoints, int attackPoints,int killCount,Action<IEnemyCharacter> onDeath)
        {
            Action<ICharacter> OnCharacterDeath = (character) =>
            {
                KillCount++;
                onDeath(this);
            };
            Id = id;
            XpReward = xpReward;
            KillCount = killCount;
            _characterImplementation = new Character(name,healthPoints,currentHealthPoints,armorPoints,attackPoints,OnCharacterDeath);
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
            _enemyCharacterImplementation.Die();
        }

        public void ModifyAttack(int quantity)
        {
            
        }

        public void ModifyArmor(int quantity)
        {
            
        }

        public void ModifyHealthPoints(int quantity)
        {
            
        }
    }
}