namespace Game.Data
{
    public interface IPlayerCharacter : ICharacter
    {
        int Level { get; }
        int ExperiencePoints { get; }
        int DeathCount { get; }

        void EarnExperience(int quantity);
        void LevelUp();
    }
}