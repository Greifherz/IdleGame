namespace Game.Data
{
    public interface IPlayerCharacter : ICharacter
    {
        int Level { get; }
        int ExperiencePoints { get; }
    }
}