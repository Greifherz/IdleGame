using System;
using Game.Data;

namespace Game.Scripts.Data
{
    public class PlayerCharacterTempStatsDecorator : IPlayerCharacter
    {
        private IPlayerCharacter Decoratee;

        public string Name => Decoratee.Name;
        public int CurrentHealthPoints => Decoratee.CurrentHealthPoints;
        public float HealthPercentage => Decoratee.HealthPercentage;
        public int Level => Decoratee.Level;
        public int ExperiencePoints => Decoratee.ExperiencePoints;
        public int DeathCount => Decoratee.DeathCount;

        public int HealthPoints => Decoratee.HealthPoints + healthPoints;
        public int ArmorPoints => Decoratee.ArmorPoints + armorPoints;
        public int AttackPoints => Decoratee.AttackPoints + attackPoints;
        public int PointsToDistribute => pointsToDistribute;
        
        private int healthPoints = 0;
        private int armorPoints = 0;
        private int attackPoints = 0;
        private int pointsToDistribute = 0;

        public PlayerCharacterTempStatsDecorator(IPlayerCharacter decoratee,ref Func<IPlayerCharacter> undecorateFunc)
        {
            Decoratee = decoratee;
            pointsToDistribute = decoratee.PointsToDistribute;
            undecorateFunc = () =>
            {
                return Decoratee;
            };
        }
        
        public void EarnExperience(int quantity)
        {
            Decoratee.EarnExperience(quantity);
        }
        
        public void TakeDamage(int damage)
        {
            Decoratee.TakeDamage(damage);
        }

        public void RestoreHealth()
        {
            Decoratee.RestoreHealth();
        }

        public void Die()
        {
            Decoratee.Die();
        }

        public void LevelUp()
        {
            Decoratee.LevelUp();
        }

        public PlayerCharacter GetConcrete()
        {
            return (PlayerCharacter)Decoratee;
        }

        public IPlayerCharacter Undecorate()
        {
            return Decoratee;
        }

        public void ModifyAttack(int quantity)
        {
            pointsToDistribute -= quantity;
            attackPoints += quantity;
        }

        public void ModifyArmor(int quantity)
        {
            pointsToDistribute -= quantity;
            armorPoints += quantity;
        }

        public void ModifyHealthPoints(int quantity)
        {
            pointsToDistribute -= quantity;
            healthPoints += quantity * 5;
        }
    }
}