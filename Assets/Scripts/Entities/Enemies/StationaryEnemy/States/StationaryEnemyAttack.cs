using Projectiles;
using UnityEngine;
using Utility;

namespace Entities.Enemies.StationaryEnemy.States
{
    public class StationaryEnemyAttack : StationaryEnemyState
    {
        private bool _hasDetectedPlayer;
        
        public StationaryEnemyAttack(StationaryEnemyController controller) : base(controller)
        {
        }
        
        public override void Enter()
        {
            Controller.spriteRenderer.color = Color.magenta;
            
            GameObject instance = Object.Instantiate(
                original: Controller.weapon.prefab,
                position: Controller.weapon.pivotPoint.position,
                rotation: Controller.transform.rotation
            );
            
            instance.GetComponent<StationaryEnemyProjectile>().Setup(
                Controller.weapon.damage, 
                Controller.weapon.enemyKnockbackForce
            );
        }

        public override void Update()
        {
            _hasDetectedPlayer = DetectCollider(UnityTag.Player);
            CollidersInAggroRange(UnityTag.Player);
            
            RotateTowardsTarget();
        }

        protected override void SetTransitions()
        {
            AddTransition(StationaryEnemyStateType.Idle, () => _hasDetectedPlayer == false);
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