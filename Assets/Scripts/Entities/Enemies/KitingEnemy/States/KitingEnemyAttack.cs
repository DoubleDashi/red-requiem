using Projectiles;
using UnityEngine;
using Utility;

namespace Entities.Enemies.KitingEnemy.States
{
    public class KitingEnemyAttack : KitingEnemyState
    {
        private bool _inAggroRange;
        private Transform _playerTransform;
        private Rigidbody2D _playerRigidbody;

        public KitingEnemyAttack(KitingEnemyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            CollidersInAggroRange(UnityTag.Player);
            _playerTransform = AggroTargetCollider.gameObject.transform;
            _playerRigidbody = _playerTransform.GetComponentInParent<Rigidbody2D>();

            Vector2 predictedPosition = PredictPlayerPosition();
            ShootProjectile(predictedPosition);

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

            Vector2 direction = ((Vector3)PredictPlayerPosition() - Controller.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(0f, 0f, angle);

            Controller.transform.rotation = Quaternion.RotateTowards(Controller.transform.rotation, target, Controller.stats.rotationSpeed * Time.deltaTime);
        }

        private Vector2 PredictPlayerPosition()
        {
            float projectileSpeed = Controller.weapon.prefab.GetComponent<KitingEnemyProjectile>().speed;
            Vector2 playerPosition = _playerTransform.position;
            Vector2 playerVelocity = _playerRigidbody.linearVelocity;
            float projectileTravelTime = Vector2.Distance(Controller.transform.position, playerPosition) / projectileSpeed;

            return playerPosition + playerVelocity * projectileTravelTime;
        }

        private void ShootProjectile(Vector2 targetPosition)
        {
            Vector2 direction = (targetPosition - (Vector2)Controller.weapon.pivotPoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            GameObject instance = Object.Instantiate(
                original: Controller.weapon.prefab,
                position: Controller.weapon.pivotPoint.position,
                rotation: rotation
            );

            instance.GetComponent<KitingEnemyProjectile>().Setup(
                Controller.weapon.damage,
                Controller.weapon.enemyKnockbackForce,
                Controller.weapon.shakeIntensity
            );
        }
    }
}