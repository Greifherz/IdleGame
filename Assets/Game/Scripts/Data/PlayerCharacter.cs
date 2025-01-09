using System;
using Game.Data.PersistentData;
using UnityEngine;

namespace Game.Data
{
    public class PlayerCharacter : IPlayerCharacter
    {
        private ICharacter _characterImplementation;

        private Action<IPlayerCharacter> _onLevelUp;

        public string Name { get; private set; }
        public int HealthPoints => _characterImplementation.HealthPoints;

        public int CurrentHealthPoints => _characterImplementation.CurrentHealthPoints;

        public int ArmorPoints => _characterImplementation.ArmorPoints;

        public int AttackPoints => _characterImplementation.AttackPoints;
        public float HealthPercentage => _characterImplementation.HealthPercentage;

        public int PointsToDistribute { get; private set; }

        public int Level { get; private set; }
        public int ExperiencePoints { get; private set; }
        public int DeathCount { get; private set; }

        public PlayerCharacter(PlayerPersistentData data, Action<IPlayerCharacter> onDeath,Action<IPlayerCharacter> onLevelUp)
        {
            Action<ICharacter> OnCharacterDeath = (character) =>
            {
                DeathCount++;
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
            Debug.Log($"Gained XP - {quantity}");
            ExperiencePoints += quantity;
        }

        public void LevelUp()
        {
            Debug.Log("Ding - LevelUp");
            Level++;
            PointsToDistribute += 1;
            ExperiencePoints = 0;
        }

        public void ModifyAttack(int quantity = 1)
        {
            PointsToDistribute -= quantity;
            _characterImplementation.ModifyAttack(quantity);
        }

        public void ModifyArmor(int quantity = 1)
        {
            PointsToDistribute -= quantity;
            _characterImplementation.ModifyArmor(quantity);
        }

        public void ModifyHealthPoints(int quantity = 1)
        {
            PointsToDistribute -= quantity;
            _characterImplementation.ModifyHealthPoints(quantity * 5);
        }

        public PlayerCharacter GetConcrete()
        {
            return this;
        }

        public void Die()
        {
            _characterImplementation.Die();
        }
    }
}