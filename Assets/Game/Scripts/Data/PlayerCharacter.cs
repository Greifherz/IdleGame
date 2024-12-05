using System;
using Game.Data.PersistentData;

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
        public int DeathCount { get; private set; }

        public PlayerCharacter(PlayerPersistentData data, Action<IPlayerCharacter> onDeath)
        {
            Action<ICharacter> OnCharacterDeath = (character) =>
            {
                onDeath(this);
            };
            DeathCount = data.DeathCount;
            Name = data.Name;
            Level = data.Level;
            ExperiencePoints = data.ExperiencePoints;
            _characterImplementation = new Character(Name,data.HealthPoints,data.CurrentHealthPoints,data.ArmorPoints,data.AttackPoints,OnCharacterDeath);
        }
        
        public void TakeDamage(int damage)
        {
            _characterImplementation.TakeDamage(damage);
        }

        public void RestoreHealth()
        {
            _characterImplementation.RestoreHealth();
        }

        public void EarnExperience(int quantity)
        {
            ExperiencePoints += quantity;
        }

        public void LevelUp()
        {
            Level++;
            ExperiencePoints = 0;
        }

        public PlayerCharacter GetConcrete()
        {
            return this;
        }

        public void Die()
        {
            DeathCount++;
        }
    }
}