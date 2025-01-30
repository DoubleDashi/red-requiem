using System;
using Animations;
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
        public static event Action OnMorphAnimationCompleted;
        
        private Animator animator { get; set; }
        private PlayerAnimationName currentAnimationName { get; set; }
        private MorphType morphType { get; set; }
        private MorphType newMorph { get; set; }
        private bool morphPlayed { get; set; }

        public PlayerAnimator(Animator animator, MorphType currentMorph)
        {
            this.animator = animator;
            morphType = currentMorph;

            SimpleAnimationStateBehaviour.OnAnimationCompleted += HandleOnAnimationCompleted;
        }

        public void PlayAnimation(PlayerAnimationName animationName)
        {
            if (animationName != currentAnimationName)
            {
                currentAnimationName = animationName;
                animator.Play(animationName.ToString());
            }
        }

        public void PlayMorphAnimation(MorphType toMorph)
        {
            morphPlayed = false;
            animator.Play(morphType + "ToSlime");
            newMorph = toMorph;
        }

        public void UpdateBlendTree(MorphType currentMorph)
        {
            animator.SetFloat(AnimationParameterName.AnimMoveX.ToString(), PlayerInput.movementDirection.x);
            animator.SetFloat(AnimationParameterName.AnimMoveY.ToString(), PlayerInput.movementDirection.y);
            animator.SetFloat(AnimationParameterName.MorphType.ToString(), (float)currentMorph);
        }
        
        public int GetCurrentMorphAnimationHash()
        {
            return Animator.StringToHash("SlimeTo" + morphType);
        }

        public int GetCurrentAnimationHash()
        {
            return Animator.StringToHash(currentAnimationName.ToString());
        }

        private void HandleOnAnimationCompleted(int animationHash)
        {
            if (animationHash == Animator.StringToHash(morphType + "ToSlime") && morphPlayed == false)
            {
                animator.Play("SlimeTo" + newMorph);
                morphType = newMorph;    
                OnMorphAnimationCompleted?.Invoke();
                morphPlayed = true;
            }
        }
    }
}