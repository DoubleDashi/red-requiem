using System.Collections;
using UnityEngine;
using Utility;

namespace Entities.MeleeEnemy.States
{
    public class MeleeEnemyAlert : MeleeEnemyState
    {
        private bool _isComplete;
        private bool _inAggroRange;
        private bool _inAttackRange;
        
        public MeleeEnemyAlert(MeleeEnemyController controller) : base(controller)
        {
        }
        
        public override void Enter()
        {
            _isComplete = false;
            
            Controller.spriteRenderer.color = Color.magenta;
            Controller.StartCoroutine(AlertRoutine());
        }

        public override void Update()
        {
            _inAggroRange = CollidersInAggroRange(UnityTag.Player);
            _inAttackRange = CollidersInAttackRange(UnityTag.Player);
        }

        protected override void SetTransitions()
        {
            AddTransition(MeleeEnemyStateType.Attack, () => _isComplete && _inAttackRange && Controller.weapon.onCooldown == false);
            AddTransition(MeleeEnemyStateType.Chase, () => _isComplete && _inAggroRange);
            AddTransition(MeleeEnemyStateType.Idle, () => _isComplete && _inAggroRange == false);
        }

        private IEnumerator AlertRoutine()
        {
            yield return new WaitForSeconds(0.25f);
            _isComplete = true;
        }
    }
}