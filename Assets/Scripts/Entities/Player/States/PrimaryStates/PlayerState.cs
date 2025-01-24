using Entities.Player.Factories;
using FSM;

namespace Entities.Player.States.PrimaryStates
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