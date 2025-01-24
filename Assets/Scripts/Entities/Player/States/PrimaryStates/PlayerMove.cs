using Entities.Player.Factories;
using Entities.Player.States.PrimaryStates;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerMove : PlayerState
    {
        public PlayerMove(PlayerController controller) : base(controller)
        {
        }

        public override void FixedUpdate()
        {
            Controller.Movement.Accelerate();
            Controller.Movement.Decelerate();
            Controller.Movement.Brake();
            Controller.Movement.SetLinearVelocity();
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => PlayerInput.movementDirection == Vector2.zero && Controller.body.linearVelocity == Vector2.zero);
            AddTransition(PlayerStateType.Morph, () => Input.GetKey(KeyCode.Mouse1));
            AddTransition(PlayerStateType.Attack, () => Input.GetKeyDown(KeyCode.Mouse0));
        }
    }
}