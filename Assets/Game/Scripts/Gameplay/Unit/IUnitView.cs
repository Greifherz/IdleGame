using UnityEngine;

namespace Game.Gameplay
{
    public interface IUnitView
    {
        void SetAmount(string amount);
        SpriteRenderer GetSpriteRenderer();
        void MoveTo(Vector3 direction);
        Vector3 GetPosition();
    }
}