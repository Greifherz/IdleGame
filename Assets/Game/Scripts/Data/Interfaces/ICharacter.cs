namespace Game.Data
{
    public interface ICharacter
    {
        string Name { get; }
        int HealthPoints { get; }
        int CurrentHealthPoints { get; }
        int ArmorPoints { get; }
        int AttackPoints { get; }
        float HealthPercentage { get; }
        
        void TakeDamage(int damage);
        
    }
}