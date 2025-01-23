using Entities.Player.Components;
using Entities.Player.Controllers;
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
            Controller.components.Movement.Accelerate();
            Controller.components.Movement.Decelerate();
            Controller.components.Movement.Brake();
            Controller.components.Movement.SetLinearVelocity();
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => PlayerInput.movementDirection == Vector2.zero && Controller.components.body.linearVelocity == Vector2.zero);
            AddTransition(PlayerStateType.Morph, () => Input.GetKey(KeyCode.Mouse1));
            AddTransition(PlayerStateType.Attack, () => Input.GetKeyDown(KeyCode.Mouse0));
        }
    }
}