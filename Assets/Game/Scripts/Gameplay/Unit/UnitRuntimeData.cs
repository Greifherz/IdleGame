using System;
using Game.Scripts.Army;

namespace Game.Gameplay
{
    public class UnitRuntimeData
    {
        public UnitBehavior Behavior { get; }
        public IUnitView View { get; }
        public int MaxHealth { get; }
        public int CurrentHealth { get; private set; }
        public int StartingAmount { get; }
        public int Amount { get; private set; }

        private ArmyUnitData _data;

        private Action OnAmountReduced;
        private Action OnUnitKilled;

        public UnitRuntimeData(UnitBehavior behavior, IUnitView view, ArmyUnitData data,int amount)
        {
            StartingAmount = Amount = amount;
            CurrentHealth = MaxHealth = data.Health;
            _data = data;
            Behavior = behavior;
            View = view;
        }

        public void Ressurrect(int totalHeal)
        {
            if (totalHeal <= 0 || Amount >= StartingAmount) return;

            // 1. Calculate the current total health of the stack.
            int currentHealthPool = CurrentHealth + (Amount - 1) * MaxHealth;

            // 2. Add the incoming heal to the pool.
            int newHealthPool = currentHealthPool + totalHeal;

            // 3. Calculate the absolute maximum possible health for this stack.
            int maxPossibleHealth = StartingAmount * MaxHealth;

            // 4. Cap the new health pool at the maximum.
            if (newHealthPool > maxPossibleHealth)
            {
                CurrentHealth = MaxHealth;
                Amount = StartingAmount;
                return;
            }

            // 5. Recalculate Amount and CurrentHealth from the new total pool,
            //    using the exact same logic as TakeDamage.
            Amount = (newHealthPool - 1) / MaxHealth + 1;
            CurrentHealth = (newHealthPool - 1) % MaxHealth + 1;
        }

        public void Heal(int heal)
        {
            CurrentHealth += heal;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int totalDamage)
        {
            if (Amount <= 0) return;

            // Calculate total health of the stack (top unit's partial health + full health of others)
            int totalHealthPool = CurrentHealth + (Amount - 1) * MaxHealth;

            if (totalDamage >= totalHealthPool)
            {
                // All units are killed
                Amount = 0;
                CurrentHealth = 0;
                OnUnitKilled?.Invoke();
                return;
            }

            // Some units survive, calculate remaining health
            int remainingHealthPool = totalHealthPool - totalDamage;
    
            // Calculate new amount and current health
            Amount = (remainingHealthPool - 1) / MaxHealth + 1;
            CurrentHealth = (remainingHealthPool - 1) % MaxHealth + 1;
        }
        
    }
}