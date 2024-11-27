using System;

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
        
        public PlayerCharacter(string name, int level, int experiencePoints,int healthPoints, int armorPoints, int attackPoints,Action<IPlayerCharacter> onDeath)
        {
            Action<ICharacter> onCharacterDeath = (character) =>
            {
                onDeath((IPlayerCharacter)character);
            };
            Name = name;
            Level = level;
            ExperiencePoints = experiencePoints;
            _characterImplementation = new Character(name,healthPoints,armorPoints,attackPoints,onCharacterDeath);
        }

        public void SetOnDeathCallback(Action<IPlayerCharacter> onDeath)
        {
            Action<ICharacter> onCharacterDeath = (character) =>
            {
                onDeath((IPlayerCharacter)character);
            };
            _characterImplementation = new Character(Name,HealthPoints,ArmorPoints,AttackPoints,onCharacterDeath);
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