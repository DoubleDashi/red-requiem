using Entities.Player.Morphs;
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
            Controller.CurrentMorph.Attack();   
        }

        public override void Update()
        {
            if (Controller.CurrentMorph is SpearMorph spearMorph)
            {
                spearMorph.Update();
            }
        }

        public override void Exit()
        {
            if (Controller.CurrentMorph is SpearMorph spearMorph)
            {
                spearMorph.Exit();
            }
        }
        
        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => Controller.CurrentMorph.IsFinished());
        }
    }
}