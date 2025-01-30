using Entities.Player.Factories;
using UnityEngine;
using Utility;

namespace Entities.Player.States.PrimaryStates
{
    public class PlayerMove : PlayerState
    {
        private bool _forceMorph;
        
        public PlayerMove(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            if (Controller.stats.bloodResource < Controller.morph.config.bloodCost)
            {
                Controller.morphKey = KeyCode.Alpha1;
                _forceMorph = true;
            }
            else
            {
                Controller.Animator.PlayAnimation(PlayerAnimationName.Run);
            }
        }

        public override void FixedUpdate()
        {
            Controller.Movement.Accelerate();
            Controller.Movement.Decelerate();
            Controller.Movement.Brake();
            Controller.Movement.SetLinearVelocity();
        }
        
        public override void Exit()
        {
            _forceMorph = false;
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => PlayerInput.movementDirection == Vector2.zero && Controller.body.linearVelocity == Vector2.zero);
            AddTransition(PlayerStateType.Morph, () => _forceMorph || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4));
            AddTransition(PlayerStateType.Attack, () => Input.GetKeyDown(KeyCode.Mouse0) && Controller.stats.bloodResource >= Controller.morph.config.bloodCost);
        }
    }
}