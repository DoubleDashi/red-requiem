using Entities.Player.States;
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
            AddState(PlayerStateType.Idle, new PlayerIdle(_controller));
            AddState(PlayerStateType.Move, new PlayerMove(_controller));
            AddState(PlayerStateType.Charge, new PlayerCharge(_controller));
            AddState(PlayerStateType.Attack, new PlayerAttack(_controller));
            
            AddState(PlayerStateType.Hurt, new PlayerHurt(_controller));
        }
    }
}