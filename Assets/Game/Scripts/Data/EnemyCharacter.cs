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

        public EnemyCharacter(int id,string name,int xpReward,int healthPoints, int armorPoints, int attackPoints,Action<IEnemyCharacter> onDeath)
        {
            Action<ICharacter> OnCharacterDeath = (character) =>
            {
                onDeath((IEnemyCharacter)character);
            };
            Id = id;
            XpReward = xpReward;
            _characterImplementation = new Character(name,healthPoints,armorPoints,attackPoints,OnCharacterDeath);
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
            KillCount++;
            _enemyCharacterImplementation.Die();
        }
    }
}