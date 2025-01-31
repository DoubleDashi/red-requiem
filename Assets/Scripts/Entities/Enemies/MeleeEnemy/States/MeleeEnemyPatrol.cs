using UnityEngine;
using Utility;

namespace Entities.Enemies.MeleeEnemy.States
{
    public class MeleeEnemyPatrol : MeleeEnemyState
    {
        private Vector2 _targetPoint;
        
        private bool _reachedTargetPoint;
        private bool _inAggroRange;
        private bool _inAttackRange;
        
        public MeleeEnemyPatrol(MeleeEnemyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.Animator.PlayAnimation(PlayerAnimationName.Run);
            
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
            Vector2 direction = ((Vector3)_targetPoint - Controller.transform.position).normalized;
            Controller.moveDir = new Vector2(Mathf.Sign(direction.x), Mathf.Sign(direction.y));
            
            //RotateTowardsTargetPosition();
            
            _reachedTargetPoint = Vector2.Distance(Controller.transform.position, _targetPoint) < 0.1f;
        }

        protected override void SetTransitions()
        {
            AddTransition(MeleeEnemyStateType.Alert, () => _inAggroRange || _inAttackRange);
            AddTransition(MeleeEnemyStateType.Idle, () => _reachedTargetPoint);
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