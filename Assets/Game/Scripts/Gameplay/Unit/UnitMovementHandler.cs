using UnityEngine;

namespace Game.Gameplay
{
    public class UnitMovementHandler
    {
        private readonly IUnitView _view;
        private readonly int _moveSpeed;

        public UnitMovementHandler(IUnitView view, int moveSpeed)
        {
            _view = view;
            _moveSpeed = moveSpeed;
        }

        public void MoveTowards(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - _view.GetPosition();
            Vector3 moveAmount = direction.normalized * (_moveSpeed * Time.deltaTime);
            _view.MoveTo(moveAmount);
        }
    }
}