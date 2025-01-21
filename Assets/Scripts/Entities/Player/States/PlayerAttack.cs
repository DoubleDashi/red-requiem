using Entities.Player.Morphs.Strategies;

namespace Entities.Player.States
{
    public class PlayerAttack : PlayerState
    {
        private float _decelerationSpeed;
        
        public PlayerAttack(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.components.CurrentMorph.Attack();   
        }

        public override void Update()
        {
            if (Controller.components.CurrentMorph is SpearMorph spearMorph)
            {
                spearMorph.Update();
            }
        }

        public override void Exit()
        {
            if (Controller.components.CurrentMorph is SpearMorph spearMorph)
            {
                spearMorph.Exit();
            }
        }
        
        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => Controller.components.CurrentMorph.IsFinished());
        }
    }
}