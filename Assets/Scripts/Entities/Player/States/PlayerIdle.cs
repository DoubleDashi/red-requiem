using Entities.Player.Components;
using Entities.Player.Controllers;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerIdle : PlayerState
    {
        public PlayerIdle(PlayerController controller) : base(controller)
        {
        }

        public override void Update()
        {
            Controller.components.Movement.ForceDecelerate();
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Move, () => PlayerInput.movementDirection != Vector2.zero);
            AddTransition(PlayerStateType.Morph, () => Input.GetKey(KeyCode.Mouse1));
            AddTransition(PlayerStateType.Attack, () => Input.GetKeyDown(KeyCode.Mouse0));
        }
    }
}
