using UnityEngine;
using Utility;

namespace Entities.Enemies.KitingEnemy.States
{
    public class KitingEnemyPatrol : KitingEnemyState
    {
        private Vector2 _targetPoint;
        
        private bool _reachedTargetPoint;
        private bool _inAggroRange;
        private bool _inAttackRange;
        
        public KitingEnemyPatrol(KitingEnemyController controller) : base(controller)
        {
        }
        
        public override void Enter()
        {
            _targetPoint = GetRandomPointInPatrolArea();
        }
        
        public override void Update()
        {
            _inAggroRange = CollidersInAggroRange(UnityTag.Player);
            _inAttackRange = CollidersInAttackRange(UnityTag.Player);
            
            Controller.transform.position = Vector2.MoveTowards(
                Controller.transform.position,
                _targetPoint,
                Controller.stats.movementSpeed * Time.deltaTime
                );
            RotateTowardsTargetPosition();
            
            _reachedTargetPoint = Vector2.Distance(Controller.transform.position, _targetPoint) < 0.1f;
        }

        protected override void SetTransitions()
        {
            AddTransition(KitingEnemyStateType.Alert, () => _inAggroRange || _inAttackRange);
            AddTransition(KitingEnemyStateType.Idle, () => _reachedTargetPoint);
        }
        
        private Vector2 GetRandomPointInPatrolArea()
        {
            float randomX = Random.Range(-Controller.patrolArea.x / 2f, Controller.patrolArea.x / 2f);
            float randomY = Random.Range(-Controller.patrolArea.y / 2f, Controller.patrolArea.y / 2f);
            
            return new Vector2(Controller.transform.position.x + randomX, Controller.transform.position.y + randomY);
        }
        
        private void RotateTowardsTargetPosition()
        {
            if (_targetPoint != Vector2.zero)
            {
                Vector2 direction = (_targetPoint - (Vector2)Controller.transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion target = Quaternion.Euler(0f, 0f, angle);
                
                Controller.transform.rotation = Quaternion.RotateTowards(Controller.transform.rotation, target, Controller.stats.rotationSpeed * Time.deltaTime);
            }
        }
    }
}