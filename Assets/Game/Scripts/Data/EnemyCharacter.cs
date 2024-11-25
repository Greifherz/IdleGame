﻿namespace Game.Data
{
    public class EnemyCharacter : ICharacter
    {
        private ICharacter _characterImplementation;
        public string Name => _characterImplementation.Name;

        public int HealthPoints => _characterImplementation.HealthPoints;

        public int CurrentHealthPoints => _characterImplementation.CurrentHealthPoints;

        public int ArmorPoints => _characterImplementation.ArmorPoints;

        public int AttackPoints => _characterImplementation.AttackPoints;
        public float HealthPercentage => _characterImplementation.HealthPercentage;

        public int XpReward { get; private set; }

        public EnemyCharacter(string name,int xpReward,int healthPoints, int armorPoints, int attackPoints)
        {
            XpReward = xpReward;
            _characterImplementation = new Character(name,healthPoints,armorPoints,attackPoints);
        }
        
        public void TakeDamage(int damage)
        {
            _characterImplementation.TakeDamage(damage);
        }
    }
}