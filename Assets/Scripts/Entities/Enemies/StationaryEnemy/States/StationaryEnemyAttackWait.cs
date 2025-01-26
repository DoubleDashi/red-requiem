using System.Collections;
using UnityEngine;
using Utility;

namespace Entities.Enemies.StationaryEnemy.States
{
    public class StationaryEnemyAttackWait : StationaryEnemyState
    {
        private bool _inAggroRange;
        
        public StationaryEnemyAttackWait(StationaryEnemyController controller) : base(controller)
        {
        }
        
        public override void Enter()
        {
            Controller.spriteRenderer.color = Color.red;
            Controller.StartCoroutine(CooldownRoutine());
        }

        public override void Update()
        {
            _inAggroRange = CollidersInAggroRange(UnityTag.Player);
            
            RotateTowardsTarget();
        }

        protected override void SetTransitions()
        {
            AddTransition(StationaryEnemyStateType.Attack, () => Controller.weapon.onCooldown == false && _inAggroRange);
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