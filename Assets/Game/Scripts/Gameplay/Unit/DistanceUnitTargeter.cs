using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public interface IUnitTargeter
    {
        IUnitView GetTarget(IUnitView unit);
    }

    public class DistanceUnitTargeter : IUnitTargeter
    {
        private TargetProviderDelegate _possibleTargetsProvider;
        
        public DistanceUnitTargeter(TargetProviderDelegate possibleTargetsProvider)
        {
            _possibleTargetsProvider = possibleTargetsProvider;
        }
        
        public IUnitView GetTarget(IUnitView unit)
        {
            var possibleTargets = _possibleTargetsProvider.Invoke();
            // No targets to choose from
            if (possibleTargets == null || possibleTargets.Count == 0)
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