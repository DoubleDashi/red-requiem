using System.Collections;
using UnityEngine;
using Utility;

namespace Entities.Enemies.MeleeEnemy.States
{
    public class MeleeEnemyIdle : MeleeEnemyState
    {
        private bool _isComplete;
        private bool _inAggroRange;
        private bool _inAttackRange;
        
        public MeleeEnemyIdle(MeleeEnemyController controller) : base(controller)
        {
        }
        
        public override void Enter()
        {
            Controller.Animator.PlayAnimation(PlayerAnimationName.Idle);
            
            _isComplete = false;
            
            Controller.body.linearVelocity = Vector2.zero;
            Controller.spriteRenderer.color = Color.yellow;
            
            Controller.StartCoroutine(IdleRoutine());
        }
        
        public override void Update()
        {
            _inAggroRange = CollidersInAggroRange(UnityTag.Player);
            _inAttackRange = CollidersInAttackRange(UnityTag.Player);
        }
        
        protected override void SetTransitions()
        {
            AddTransition(MeleeEnemyStateType.Alert, () => _inAggroRange || _inAttackRange);
            AddTransition(MeleeEnemyStateType.Patrol, () => _isComplete && _inAggroRange == false);
        }
        
        private IEnumerator IdleRoutine()
        {
            yield return new WaitForSeconds(2f);
            _isComplete = true;
        }
    }
}