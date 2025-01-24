using Entities.Player.Factories;

namespace Entities.Player.States.PrimaryStates
{
    public class PlayerAttack : PlayerState
    {
        private bool _isComplete;
        
        public PlayerAttack(PlayerController controller) : base(controller)
        {
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.SpearCharge, () => Controller.morph.config.type == MorphType.Spear);
            AddTransition(PlayerStateType.ShardAttack, () => Controller.morph.config.type == MorphType.Shard);
            AddTransition(PlayerStateType.SwordAttack, () => Controller.morph.config.type == MorphType.Sword);
            AddTransition(PlayerStateType.ScytheAttack, () => Controller.morph.config.type == MorphType.Scythe);
            AddTransition(PlayerStateType.CannonCharge, () => Controller.morph.config.type == MorphType.Cannon);
        }
    }
}