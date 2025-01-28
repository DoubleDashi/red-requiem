using Animations;
using Entities.Player.Factories;
using UnityEngine;
using Utility;

namespace Entities.Player.States.MorphStates
{
    public class PlayerSwordAttack : MorphState
    {
        private bool _isComplete;
        
        public PlayerSwordAttack(PlayerController controller) : base(controller)
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
            Controller.Animator.PlayAnimation(PlayerAnimationName.Attack);
        }

        public override void Update()
        {
            CollisionDetection();
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
    }
}