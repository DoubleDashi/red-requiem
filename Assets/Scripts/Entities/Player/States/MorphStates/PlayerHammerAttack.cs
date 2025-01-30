using System.Collections;
using Animations;
using Configs.Events;
using Entities.Player.Factories;
using UnityEngine;
using Utility;

namespace Entities.Player.States.MorphStates
{
    public class PlayerHammerAttack : MorphState
    {
        private bool _isComplete;
        
        public PlayerHammerAttack(PlayerController controller) : base(controller)
        {
        }
        
        public override void Subscribe()
        {
            SimpleAnimationStateBehaviour.OnAnimationCompleted += HandleOnAnimationCompleted;
            SimpleAnimationStateBehaviour.OnAnimationTriggerActivated += HandleOnAnimationTriggerActivated;
        }
        
        public override void Unsubscribe()
        {
            SimpleAnimationStateBehaviour.OnAnimationCompleted -= HandleOnAnimationCompleted;
            SimpleAnimationStateBehaviour.OnAnimationTriggerActivated -= HandleOnAnimationTriggerActivated;
        }

        public override void Enter()
        {
            Controller.Animator.PlayAnimation(PlayerAnimationName.Attack);
        }
        
        public override void FixedUpdate()
        {
            Controller.Movement.ForceDecelerate();
        }

        public override void Exit()
        {
            CollisionClear();
            _isComplete = false;
        }
        
        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => _isComplete && Controller.body.linearVelocity == Vector2.zero);
            AddTransition(PlayerStateType.Move, () => _isComplete && Controller.body.linearVelocity != Vector2.zero);
        }
        
        private void HandleOnAnimationCompleted(int shortNameHash)
        {
            if (shortNameHash == Animator.StringToHash(PlayerAnimationName.Attack.ToString()))
            {
                _isComplete = true;
            }
        }

        private void HandleOnAnimationTriggerActivated(int shortNameHash)
        {
            if (shortNameHash == Animator.StringToHash(PlayerAnimationName.Attack.ToString()) && Controller.morph.config.type == MorphType.Hammer)
            {
                PlayerEventConfig.OnHammerDownSFX?.Invoke(Controller.stats.guid);
                CameraEventConfig.OnShake?.Invoke(Controller.morph.config.shakeIntensity);
                CollisionDetection();
            }
        }
    }
}