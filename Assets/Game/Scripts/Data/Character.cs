using System;
using UnityEngine;

namespace Game.Data
{
    public class Character : ICharacter
    {
        public string Name { get; private set; }
        public int HealthPoints { get; private set; }
        public int CurrentHealthPoints { get; private set; }
        public int ArmorPoints { get; private set; }
        public int AttackPoints { get; private set; }
        
        public float HealthPercentage => (float)CurrentHealthPoints / HealthPoints;

        private Action<ICharacter> _onCharacterDeath;

        public Character(string name,int healthPoints, int armorPoints, int attackPoints,Action<ICharacter> onCharacterDeath)
        {
            Name = name;
            CurrentHealthPoints = HealthPoints = healthPoints;
            ArmorPoints = armorPoints;
            AttackPoints = attackPoints;
            _onCharacterDeath = onCharacterDeath;
        }
    
        public Character(int healthPoints, int armorPoints, int attackPoints,Action<ICharacter> onCharacterDeath)
        {
            CurrentHealthPoints = HealthPoints = healthPoints;
            ArmorPoints = armorPoints;
            AttackPoints = attackPoints;
            _onCharacterDeath = onCharacterDeath;
        }

        public void TakeDamage(int damage)
        {
            var DamageToTake = damage - ArmorPoints;
            if (DamageToTake < 0)
                DamageToTake = 0;
        
            CurrentHealthPoints -= DamageToTake;
        
            if (CurrentHealthPoints == 0)
            {
                Die();
            }
        }

        public void RestoreHealth()
        {
            CurrentHealthPoints = HealthPoints;
        }

        public void Die()
        {
            _onCharacterDeath(this);
            RestoreHealth();
        }
    }
}
