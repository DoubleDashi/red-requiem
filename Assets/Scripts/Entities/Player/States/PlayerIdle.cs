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
            AddTransition(PlayerStateType.Move, () => PlayerInput.MovementDirection != Vector2.zero);
            AddTransition(PlayerStateType.Charge, () => PlayerInput.ChargeKeyPressed || PlayerInput.ChargeKeyHold);
        }
    }
}