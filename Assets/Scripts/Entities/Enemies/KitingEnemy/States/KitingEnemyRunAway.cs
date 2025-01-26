using UnityEngine;
using Utility;

namespace Entities.Enemies.KitingEnemy.States
{
    public class KitingEnemyRunAway : KitingEnemyState
    {
        private bool _inAggroRange;
        private bool _inAttackRange;
        private bool _inRunAwayRange;

        private Vector2 _direction;
        
        public KitingEnemyRunAway(KitingEnemyController controller) : base(controller)
        {
        }
        
        public override void Enter()
        {
            _inAggroRange = CollidersInAggroRange(UnityTag.Player);
            Vector2 directionToPlayer = (AggroTargetCollider.transform.position - Controller.transform.position).normalized;
            _direction = -directionToPlayer;
        }

        public override void Update()
        {
            _inAggroRange = CollidersInAggroRange(UnityTag.Player);
            _inAttackRange = CollidersInAttackRange(UnityTag.Player);
            _inRunAwayRange = CollidersInRunAwayRange(UnityTag.Player);
            
            Controller.transform.position += (Vector3)(_direction * Controller.stats.movementSpeed * Time.deltaTime);
            RotateAway(_direction);
        }

        protected override void SetTransitions()
        {
            AddTransition(KitingEnemyStateType.Attack, () => _inRunAwayRange == false && _inAttackRange && Controller.weapon.onCooldown == false);
            AddTransition(KitingEnemyStateType.Chase, () => _inRunAwayRange == false && _inAttackRange == false && _inAggroRange);
            AddTransition(KitingEnemyStateType.Idle, () => _inRunAwayRange == false && _inAttackRange == false && _inAggroRange == false);
        }
        
        private void RotateAway(Vector2 oppositeDirection)
        {
            float angle = Mathf.Atan2(oppositeDirection.y, oppositeDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
            Controller.transform.rotation = Quaternion.RotateTowards(Controller.transform.rotation, targetRotation, Controller.stats.rotationSpeed * Time.deltaTime);
        }
    }
}