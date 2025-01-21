using Entities.Player.Components;
using Entities.Player.Controllers;
using FSM;

namespace Entities.Player.States
{
    public abstract class PlayerState : BaseState<PlayerStateType>
    {
        protected readonly PlayerController Controller;
        
        protected PlayerState(PlayerController controller)
        {
            Controller = controller;
        }
    }
}