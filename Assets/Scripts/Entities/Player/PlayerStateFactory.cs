using FSM;

namespace Entities.Player
{
    public class PlayerStateFactory : StateFactory<PlayerStateType>
    {
        private readonly PlayerController _controller;
        
        public PlayerStateFactory(PlayerController controller)
        {
            _controller = controller;
        }
        
        protected override void SetStates()
        {
            AddState(PlayerStateType.Idle, new States.PlayerIdle(_controller));
            AddState(PlayerStateType.Move, new States.PlayerMove(_controller));
            AddState(PlayerStateType.Charge, new States.PlayerCharge(_controller));
        }
    }
}