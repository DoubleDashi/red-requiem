using Entities.Player.Factories;
using UnityEngine;
using Utility;

namespace Entities.Player
{
    public enum AnimationParameterName
    {
        AnimMoveX,
        AnimMoveY,
        MorphType,
    }
    
    public class PlayerAnimator
    {
        private Animator animator { get; set; }
        private PlayerAnimationName currentAnimationName { get; set; }

        public PlayerAnimator(Animator animator)
        {
            this.animator = animator;
        }

        public void PlayAnimation(PlayerAnimationName animationName)
        {
            if (animationName != currentAnimationName)
            {
                currentAnimationName = animationName;
                animator.Play(animationName.ToString());
            }
        }

        public void UpdateBlendTree(MorphType currentMorph)
        {
            animator.SetFloat(AnimationParameterName.AnimMoveX.ToString(), PlayerInput.movementDirection.x);
            animator.SetFloat(AnimationParameterName.AnimMoveY.ToString(), PlayerInput.movementDirection.y);
            animator.SetFloat(AnimationParameterName.MorphType.ToString(), (float)currentMorph);
        }
    }
}