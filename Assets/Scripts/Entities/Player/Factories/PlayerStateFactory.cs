﻿using Entities.Player.States;
using Entities.Player.States.Morphs;
using Entities.Player.States.MorphStates;
using Entities.Player.States.PrimaryStates;
using FSM;

namespace Entities.Player.Factories
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
            // Primary states
            AddState(PlayerStateType.Idle, new PlayerIdle(_controller));
            AddState(PlayerStateType.Move, new PlayerMove(_controller));
            AddState(PlayerStateType.Hurt, new PlayerHurt(_controller));
            AddState(PlayerStateType.Morph, new PlayerMorph(_controller));
            AddState(PlayerStateType.Attack, new PlayerAttack(_controller));
            
            // Morph states
            AddState(PlayerStateType.SpearCharge, new PlayerSpearCharge(_controller));
            AddState(PlayerStateType.SpearAttack, new PlayerSpearAttack(_controller));
            AddState(PlayerStateType.ShardAttack, new PlayerShardAttack(_controller));
            AddState(PlayerStateType.SwordAttack, new PlayerSwordAttack(_controller));
            AddState(PlayerStateType.ScytheAttack, new PlayerScytheAttack(_controller));
            AddState(PlayerStateType.CannonCharge, new PlayerCannonCharge(_controller));
            AddState(PlayerStateType.CannonAttack, new PlayerCannonAttack(_controller));
        }
    }
}