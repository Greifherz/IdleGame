using UnityEngine;

namespace Game.Gameplay
{
    public interface IUnitTargeter
    {
        IUnitView GetTarget(IUnitView unit, IUnitView[] possibleTargets);
    }

    public class DistanceUnitTargeter : IUnitTargeter
    {
        public IUnitView GetTarget(IUnitView unit, IUnitView[] possibleTargets)
        {
            // No targets to choose from
            if (possibleTargets == null || possibleTargets.Length == 0)
            {
                return null; 
            }

            // --- Initialization ---
            IUnitView closestTarget = null;
            float closestDistanceSqr = float.MaxValue;
            Vector3 currentPosition = unit.GetPosition();

            // --- Find Closest Target ---
            foreach (var potentialTarget in possibleTargets)
            {
                // Calculate squared distance for performance
                float sqrDistance = Vector3.SqrMagnitude(potentialTarget.GetPosition() - currentPosition);

                if (sqrDistance < closestDistanceSqr)
                {
                    closestDistanceSqr = sqrDistance;
                    closestTarget = potentialTarget;
                }
            }

            return closestTarget;
        }
    }
}