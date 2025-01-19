namespace Entities.Player.States
{
    public class PlayerIdle : PlayerState
    {
        public PlayerIdle(PlayerController controller) : base(controller)
        {
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Charge, () => PlayerInput.ChargeKeyPressed || PlayerInput.ChargeKeyHold);
        }
    }
}