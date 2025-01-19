using System.Collections;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerDash : PlayerState
    {
        private bool _completed;
        
        public PlayerDash(PlayerController controller) : base(controller)
        {
        }
        
        public override void Enter()
        {
            Controller.StartCoroutine(DashRoutine());
        }

        public override void Exit()
        {
            _completed = false;
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => _completed);
        }

        private IEnumerator DashRoutine()
        {
            yield return new WaitForSeconds(2f);

            _completed = true;
        }
    }
}