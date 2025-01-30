using Animations;
using Entities.Player.Factories;
using UnityEngine;

namespace Entities.Player.States.PrimaryStates
{
    public class PlayerMorph : PlayerState
    {
        private bool _completed = false;
        private float _maxSpeed;
        
        public PlayerMorph(PlayerController controller) : base(controller)
        {
        }

        public override void Subscribe()
        {
            SimpleAnimationStateBehaviour.OnAnimationCompleted += HandleOnAnimationCompleted;
        }
        
        public override void Unsubscribe()
        {
            SimpleAnimationStateBehaviour.OnAnimationCompleted -= HandleOnAnimationCompleted;
        }

        public override void Enter()
        {
            if (Controller.morphKey != KeyCode.Alpha3)
            {
                Controller.stats.maxSpeed = Controller.originalMaxSpeed;
            }
            
            if (Controller.morphKey == KeyCode.Alpha1)
            {
                Controller.morph.config = Controller.MorphFactory.FindByType(MorphType.Sword);
                Controller.Animator.PlayMorphAnimation(MorphType.Sword);
            }
            
            if (Controller.morphKey == KeyCode.Alpha2)
            {
                Controller.morph.config = Controller.MorphFactory.FindByType(MorphType.Shard);
                Controller.Animator.PlayMorphAnimation(MorphType.Shard);
            }
            
            if (Controller.morphKey == KeyCode.Alpha3)
            {
                Controller.stats.maxSpeed = Controller.originalMaxSpeed * 0.6f;
                Controller.morph.config = Controller.MorphFactory.FindByType(MorphType.Hammer);
                Controller.Animator.PlayMorphAnimation(MorphType.Hammer);
            }
            
            if (Controller.morphKey == KeyCode.Alpha4)
            {
                Controller.morph.config = Controller.MorphFactory.FindByType(MorphType.Cannon);
                Controller.Animator.PlayMorphAnimation(MorphType.Cannon);
            }
        }
        
        public override void Update()
        {
            Controller.Movement.ForceDecelerate();
        }

        public override void Exit()
        {
            _completed = false;
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => _completed);
        }

        private void HandleOnAnimationCompleted(int animationHash)
        {
            if (Controller.Animator.GetCurrentMorphAnimationHash() == animationHash && _completed == false)
            {
                _completed = true;
            }
        }
    }
}
