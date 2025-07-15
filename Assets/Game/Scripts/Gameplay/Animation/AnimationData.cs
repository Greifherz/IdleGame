using System;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public class AnimationData
    {
        public AnimationType AnimationType;
        public Sprite[] AnimationSprites;
        public float AnimationTime;
        public bool Loop;
    }
}