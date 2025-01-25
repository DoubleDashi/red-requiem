using UnityEngine;
using Utility;

namespace Entities.MeleeEnemy.States
{
    public class MeleeEnemyChase : MeleeEnemyState
    {
        private bool _inAggroRange;
        private bool _inAttackRange;
        
        public MeleeEnemyChase(MeleeEnemyController controller) : base(controller)
        {
        }

        public override void Update()
        {
            _inAggroRange = CollidersInAggroRange(UnityTag.Player);
            _inAttackRange = CollidersInAttackRange(UnityTag.Player);

            if (AggroTargetCollider && _inAggroRange)
            {
                Controller.transform.position = Vector2.MoveTowards(
                    Controller.weapon.pivotPoint.transform.position, //Controller.transform.position,
                    AggroTargetCollider.transform.position,
                    Controller.stats.movementSpeed * Time.deltaTime
                );
                RotateTowardsTarget();
            }
        }

        protected override void SetTransitions()
        {
            AddTransition(MeleeEnemyStateType.Attack, () => _inAttackRange && Controller.weapon.onCooldown == false);
            AddTransition(MeleeEnemyStateType.Idle, () => _inAggroRange == false);
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