using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(fileName = "AnimationDatabase", menuName = "ScriptableObjects/AnimationDatabase", order = 1)]
    public class AnimationDatabase : ScriptableObject
    {
        [SerializeField] private AnimationData[] Animations;

        public AnimationData GetAnimationData(AnimationType type)
        {
            foreach (var AnimData in Animations)
            {
                if (AnimData.AnimationType == type)
                    return AnimData;
            }

            return null;
        }
    }

    public enum AnimationType
    {
        Idle,
        Move,
        Attack,
        Attack2,
        Block,
    }
}