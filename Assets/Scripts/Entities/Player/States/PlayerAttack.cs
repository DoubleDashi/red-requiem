﻿using Entities.Player.Components;

namespace Entities.Player.States
{
    public class PlayerAttack : PlayerState
    {
        private bool _isComplete;
        
        public PlayerAttack(PlayerController controller) : base(controller)
        {
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.SpearCharge, () => Controller.currentMorph.type == MorphType.Spear);
            AddTransition(PlayerStateType.ShardAttack, () => Controller.currentMorph.type == MorphType.Shard);
            AddTransition(PlayerStateType.SwordAttack, () => Controller.currentMorph.type == MorphType.Sword);
            AddTransition(PlayerStateType.ScytheAttack, () => Controller.currentMorph.type == MorphType.Scythe);
            AddTransition(PlayerStateType.CannonCharge, () => Controller.currentMorph.type == MorphType.Cannon);
        }
    }
}