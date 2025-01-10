using System;

namespace Game.Data
{
    public interface IPlayerCharacter : ICharacter
    {
        int Level { get; }
        int ExperiencePoints { get; }
        int DeathCount { get; }

        void EarnExperience(int quantity);
        void LevelUp();
        int PointsToDistribute { get; }

        event Action<IPlayerCharacter> OnPlayerLevelUp;
        event Action<IPlayerCharacter> OnPlayerDeath;

        PlayerCharacter GetConcrete();
    }
}