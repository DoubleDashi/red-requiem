using Entities.Player.Controllers;
using Entities.Player.States;
using FSM;

namespace Entities.Player.Components
{
    public enum PlayerStateType
    {
        Idle,
        Move,
        Hurt,
        Morph,
        Attack,
    }
    
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
            AddState(PlayerStateType.Hurt, new PlayerHurt(_controller));
            AddState(PlayerStateType.Morph, new PlayerMorph(_controller));
            AddState(PlayerStateType.Attack, new PlayerAttack(_controller));
        }
    }
}