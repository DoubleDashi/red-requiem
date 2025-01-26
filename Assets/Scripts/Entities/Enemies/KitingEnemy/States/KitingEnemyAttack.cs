using Projectiles;
using UnityEngine;
using Utility;

namespace Entities.Enemies.KitingEnemy.States
{
    public class KitingEnemyAttack : KitingEnemyState
    {
        private bool _inAggroRange;
        
        public KitingEnemyAttack(KitingEnemyController controller) : base(controller)
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
            
            instance.GetComponent<KitingEnemyProjectile>().Setup(
                Controller.weapon.damage, 
                Controller.weapon.enemyKnockbackForce,
                Controller.weapon.shakeIntensity
                );
            
            Controller.weapon.onCooldown = true;
        }

        public override void Update()
        {
            _inAggroRange = CollidersInAggroRange(UnityTag.Player);
            CollidersInAggroRange(UnityTag.Player);
            
            RotateTowardsTarget();
        }

        protected override void SetTransitions()
        {
            AddTransition(KitingEnemyStateType.AttackWait, () => Controller.weapon.onCooldown);
            AddTransition(KitingEnemyStateType.Idle, () => _inAggroRange == false);
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