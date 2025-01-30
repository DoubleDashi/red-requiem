using Entities.Player.Factories;
using UnityEngine;
using Utility;

namespace Entities.Player.States.PrimaryStates
{
    public class PlayerIdle : PlayerState
    {
        public PlayerIdle(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.Animator.PlayAnimation(PlayerAnimationName.Idle);
        }

        public override void Update()
        {
            if (Controller.Animator.GetCurrentAnimationHash() != Animator.StringToHash(PlayerAnimationName.Idle.ToString()))
            {
                Controller.Animator.PlayAnimation(PlayerAnimationName.Idle);
            }
        }
        
        public override void FixedUpdate()
        {
            Controller.Movement.ForceDecelerate();
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Move, () => PlayerInput.movementDirection != Vector2.zero);
            AddTransition(PlayerStateType.Morph, () => Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4));
            AddTransition(PlayerStateType.Attack, () => Input.GetKeyDown(KeyCode.Mouse0));
        }
    }
}
