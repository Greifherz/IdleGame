using System;

namespace Game.Gameplay
{
    public class UnitAttackHandler
    {
        private float _attackCooldown;
        private float _currentCooldown;

        public UnitAttackHandler(float baseCooldown)
        {
            _attackCooldown = baseCooldown;
            _currentCooldown = 0;
        }

        public bool CanAttack()
        {
            return _currentCooldown <= 0;
        }

        public void Tick(float deltaTime)
        {
            if (_currentCooldown > 0)
            {
                _currentCooldown -= deltaTime;
            }
        }

        public void PerformAttack(Action onAttackComplete)
        {
            // This is where you would trigger the attack animation and logic.
            // The callback signals when the attack is "done" so the cooldown can start.
            onAttackComplete?.Invoke();
            _currentCooldown = _attackCooldown + UnityEngine.Random.Range(-0.2f, 0.2f);
        }
    }

}