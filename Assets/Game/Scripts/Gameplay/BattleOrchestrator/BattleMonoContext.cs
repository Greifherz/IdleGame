using Game.Scripts.Gameplay.BattleOrchestrator;
using UnityEngine;

namespace Game.Gameplay
{
    public class BattleMonoContext : MonoBehaviour
    {
        [SerializeField] private UnitMonoContext[] FriendlyUnitsContext;
        [SerializeField] private UnitMonoContext[] EnemyUnitsContext;
        //Background stuff

        public BattleAggregatorContext GetBattleAggregatorContext()
        {
            return new BattleAggregatorContext(FriendlyUnitsContext,EnemyUnitsContext);
        }
    }
}