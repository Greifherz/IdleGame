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
        void RestoreHealth();
        void Die();
        void ModifyAttack(int quantity);
        void ModifyArmor(int quantity);
        void ModifyHealthPoints(int quantity);
    }
}