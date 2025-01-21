using Entities.Player.Components;
using Entities.Player.Controllers;

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
            // AddTransition(PlayerStateType.Idle, () => Controller.morph.currentMorph.IsComplete());
        }
    }
}