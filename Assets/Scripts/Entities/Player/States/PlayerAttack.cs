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

        public override void Enter()
        {
            _isComplete = false;
            Controller.attack.currentMorph.Enter();
        }
        
        public override void Update()
        {
            Controller.attack.currentMorph.Update();
            _isComplete = Controller.attack.currentMorph.IsComplete();
        }
        
        public override void Exit()
        {
            Controller.attack.currentMorph.Exit();
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => _isComplete);
        }
    }
}