using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerIdle : PlayerState
    {
        public PlayerIdle(PlayerController controller) : base(controller)
        {
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Move, () => PlayerInput.movementDirection != Vector2.zero);
            AddTransition(PlayerStateType.Charge, () => (PlayerInput.chargeKeyPressed || PlayerInput.chargeKeyHold) && Controller.CurrentMorph.HasCharge);
            AddTransition(PlayerStateType.Attack, () => PlayerInput.chargeKeyPressed && Controller.CurrentMorph.HasCharge == false);
        }
    }
}