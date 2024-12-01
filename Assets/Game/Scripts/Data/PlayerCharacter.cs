﻿using System;

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

        public PlayerCharacter(string name, int level, int experiencePoints,int healthPoints, int armorPoints, int attackPoints,Action<IPlayerCharacter> onDeath,int deathCount = 0)
        {
            Action<ICharacter> OnCharacterDeath = (character) =>
            {
                onDeath(this);
            };
            DeathCount = deathCount;
            Name = name;
            Level = level;
            ExperiencePoints = experiencePoints;
            _characterImplementation = new Character(name,healthPoints,armorPoints,attackPoints,OnCharacterDeath);
        }

        public void SetOnDeathCallback(Action<IPlayerCharacter> onDeath)
        {
            Action<ICharacter> OnCharacterDeath = (character) =>
            {
                onDeath((IPlayerCharacter)character);
            };
            _characterImplementation = new Character(Name,HealthPoints,ArmorPoints,AttackPoints,OnCharacterDeath);
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