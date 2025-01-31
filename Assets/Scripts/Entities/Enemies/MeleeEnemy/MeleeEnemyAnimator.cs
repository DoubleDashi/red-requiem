using System;
using Animations;
using Entities.Player;
using Entities.Player.Factories;
using UnityEngine;
using Utility;

namespace Entities.Enemies.MeleeEnemy
{
    
    public class EnemyAnimator
    {
        private Animator animator { get; set; }
        private PlayerAnimationName currentAnimationName { get; set; }

        public EnemyAnimator(Animator animator)
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

        public void UpdateBlendTree(Vector2 moveDir)
        {
            animator.SetFloat(AnimationParameterName.AnimMoveX.ToString(), moveDir.x);
            animator.SetFloat(AnimationParameterName.AnimMoveY.ToString(), moveDir.y);
        }
    }
}