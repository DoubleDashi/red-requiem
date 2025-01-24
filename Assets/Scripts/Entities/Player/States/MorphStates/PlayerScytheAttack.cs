using System.Collections;
using Entities.Player.Factories;
using UnityEngine;

namespace Entities.Player.States.MorphStates
{
    public class PlayerScytheAttack : MorphState
    {
        private bool _isComplete;
        
        public PlayerScytheAttack(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.StartCoroutine(AttackRoutine());
        }

        public override void Update()
        {
            CollisionDetection();
        }

        public override void FixedUpdate()
        {
            Controller.Movement.ForceDecelerate();
        }
        
        public override void Exit()
        {
            CollisionClear();
            _isComplete = false;
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => _isComplete && Controller.body.linearVelocity == Vector2.zero);
            AddTransition(PlayerStateType.Move, () => _isComplete && Controller.body.linearVelocity != Vector2.zero);
        }

        private IEnumerator AttackRoutine()
        {
            yield return new WaitForSeconds(0.15f);

            _isComplete = true;
        }
    }
}