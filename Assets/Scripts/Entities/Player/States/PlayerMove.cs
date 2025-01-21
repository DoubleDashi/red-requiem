using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerMove : PlayerState
    {
        public PlayerMove(PlayerController controller) : base(controller)
        {
        }

        public override void Update()
        {
            Controller.components.Movement.Accelerate();
            Controller.components.Movement.Decelerate();
            Controller.components.Movement.Brake();
            Controller.components.Movement.SetLinearVelocity();
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => PlayerInput.movementDirection == Vector2.zero && Controller.components.Body.linearVelocity == Vector2.zero);
            AddTransition(PlayerStateType.Charge, () => (PlayerInput.chargeKeyPressed || PlayerInput.chargeKeyHold) && Controller.components.CurrentMorph.HasCharge);
            AddTransition(PlayerStateType.Attack, () => PlayerInput.chargeKeyPressed && Controller.components.CurrentMorph.HasCharge == false);
            AddTransition(PlayerStateType.Morph, () => Input.GetKey(KeyCode.Mouse1));
        }
    }
}