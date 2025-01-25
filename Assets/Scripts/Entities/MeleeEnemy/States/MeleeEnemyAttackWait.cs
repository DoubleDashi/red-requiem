using System.Collections;
using UnityEngine;
using Utility;

namespace Entities.MeleeEnemy.States
{
    public class MeleeEnemyAttackWait : MeleeEnemyState
    {
        private bool _inAttackRange;
        
        public MeleeEnemyAttackWait(MeleeEnemyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.spriteRenderer.color = Color.red;
            Controller.StartCoroutine(CooldownRoutine());
        }

        public override void Update()
        {
            _inAttackRange = CollidersInAttackRange(UnityTag.Player);
            
            RotateTowardsTarget();
        }

        protected override void SetTransitions()
        {
            AddTransition(MeleeEnemyStateType.Chase, () => Controller.weapon.onCooldown == false && _inAttackRange == false);
            AddTransition(MeleeEnemyStateType.Attack, () => Controller.weapon.onCooldown == false && _inAttackRange);
        }

        private IEnumerator CooldownRoutine()
        {
            yield return new WaitForSeconds(Controller.weapon.cooldownTime);
            Controller.weapon.onCooldown = false;
        }
        
        private void RotateTowardsTarget()
        {
            if (AggroTargetCollider == false)
            {
                return;
            }
            
            Vector2 direction = (AggroTargetCollider.transform.position - Controller.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(0f, 0f, angle);
            
            Controller.transform.rotation = Quaternion.RotateTowards(Controller.transform.rotation, target, Controller.stats.rotationSpeed * Time.deltaTime);
        }
    }
}