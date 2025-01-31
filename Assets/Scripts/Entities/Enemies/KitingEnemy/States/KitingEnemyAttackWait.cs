using System.Collections;
using Entities.Enemies.MeleeEnemy;
using UnityEngine;
using Utility;

namespace Entities.Enemies.KitingEnemy.States
{
    public class KitingEnemyAttackWait : KitingEnemyState
    {
        private bool _inAttackRange;
        private bool _inRunAwayRange;
        
        public KitingEnemyAttackWait(KitingEnemyController controller) : base(controller)
        {
        }
        
        public override void Enter()
        {
            Controller.Animator.PlayAnimation(PlayerAnimationName.Idle);
            Controller.spriteRenderer.color = Color.red;
            Controller.StartCoroutine(CooldownRoutine());
        }

        public override void Update()
        {
            CollidersInAggroRange(UnityTag.Player);
            _inAttackRange = CollidersInAttackRange(UnityTag.Player);
            _inRunAwayRange = CollidersInRunAwayRange(UnityTag.Player);
            
            //RotateTowardsTarget();
        }

        protected override void SetTransitions()
        {
            AddTransition(KitingEnemyStateType.RunAway, () => _inRunAwayRange);
            AddTransition(KitingEnemyStateType.Chase, () => Controller.weapon.onCooldown == false && _inAttackRange == false);
            AddTransition(KitingEnemyStateType.Attack, () => Controller.weapon.onCooldown == false && _inAttackRange);
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