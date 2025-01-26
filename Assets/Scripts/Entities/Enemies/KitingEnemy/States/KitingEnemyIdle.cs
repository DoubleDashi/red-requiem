using System.Collections;
using UnityEngine;
using Utility;

namespace Entities.Enemies.KitingEnemy.States
{
    public class KitingEnemyIdle : KitingEnemyState
    {
        private bool _isComplete;
        private bool _inAggroRange;
        private bool _inAttackRange;
        
        public KitingEnemyIdle(KitingEnemyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            _isComplete = false;
            
            Controller.body.linearVelocity = Vector2.zero;
            
            Controller.StartCoroutine(IdleRoutine());
        }
        
        public override void Update()
        {
            _inAggroRange = CollidersInAggroRange(UnityTag.Player);
            _inAttackRange = CollidersInAttackRange(UnityTag.Player);
        }
        
        protected override void SetTransitions()
        {
            AddTransition(KitingEnemyStateType.Alert, () => _inAggroRange || _inAttackRange);
            AddTransition(KitingEnemyStateType.Patrol, () => _isComplete && _inAggroRange == false);
        }
        
        private IEnumerator IdleRoutine()
        {
            yield return new WaitForSeconds(2f);
            _isComplete = true;
        }
    }
}