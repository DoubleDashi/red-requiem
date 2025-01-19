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
            Controller.transform.Translate(PlayerInput.movementDirectionNormalized * Controller.stats.maxSpeed * Time.deltaTime);
        }
        
        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => PlayerInput.movementDirection == Vector2.zero);
            AddTransition(PlayerStateType.Dash, () => PlayerInput.dashPressed);
        }
    }
}