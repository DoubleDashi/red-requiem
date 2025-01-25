using System.Collections;
using UnityEngine;
using Utility;

namespace Entities.Enemies.MeleeEnemy.States
{
    public class MeleeEnemyAttack : MeleeEnemyState
    {
        private bool _isComplete;
        private bool _inAggroRange;
        private bool _inAttackRange;
        
        public MeleeEnemyAttack(MeleeEnemyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.spriteRenderer.color = Color.red;
            Controller.StartCoroutine(AttackRoutine());
        }

        public override void Update()
        {
            _inAggroRange = CollidersInAggroRange(UnityTag.Player);
            _inAttackRange = CollidersInAttackRange(UnityTag.Player);
            
            RotateTowardsTarget();
            CollisionDetection(UnityTag.Player);
        }

        public override void Exit()
        {
            CollisionClear();
            _isComplete = false;
        }

        protected override void SetTransitions()
        {
            AddTransition(MeleeEnemyStateType.AttackWait, () => _isComplete && Controller.weapon.onCooldown);
            AddTransition(MeleeEnemyStateType.Chase, () => _isComplete && _inAggroRange && _inAttackRange == false);
            AddTransition(MeleeEnemyStateType.Idle, () => _isComplete && _inAggroRange == false && _inAttackRange == false);
        }

        private IEnumerator AttackRoutine()
        {
            yield return new WaitForSeconds(0.25f);
            Controller.weapon.onCooldown = true;
            _isComplete = true;
        }
        
        private void RotateTowardsTarget()
        {
            Vector2 direction = (AggroTargetCollider.transform.position - Controller.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(0f, 0f, angle);
            
            Controller.transform.rotation = Quaternion.RotateTowards(Controller.transform.rotation, target, Controller.stats.rotationSpeed * Time.deltaTime);
        }
    }
}