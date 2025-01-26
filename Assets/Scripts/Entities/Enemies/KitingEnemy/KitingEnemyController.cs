using System;
using Configs.Events;
using Data;
using FSM;
using UnityEngine;

namespace Entities.Enemies.KitingEnemy
{
    public class KitingEnemyController : StateMachine<KitingEnemyStateType>, IEntity
    {
        public EnemyStats stats;
        public WeaponStats weapon;
        public Vector2 patrolArea;
        
        [HideInInspector] public SpriteRenderer spriteRenderer;
        [HideInInspector] public Rigidbody2D body;
        [HideInInspector] public bool isHurt;
        
        protected override void Subscribe()
        {
            KitingEnemyEventConfig.OnDeath += HandleOnDeath;
        }
        
        protected override void Unsubscribe()
        {
            KitingEnemyEventConfig.OnDeath -= HandleOnDeath;
        }
        
        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            body = GetComponent<Rigidbody2D>();
            
            InitializeStateMachine(
                new KitingEnemyStateFactory(this),
                KitingEnemyStateType.Idle
            );
        }

        protected override void SetGlobalTransitions()
        {
            AddGlobalTransition(KitingEnemyStateType.Hurt, () => isHurt);
        }

        public void TakeDamage(Damageable damageable)
        {
            isHurt = true;
            
            KitingEnemyEventConfig.OnHurt?.Invoke(stats.guid, damageable);
        }

        public void HandleOnDeath(Guid guid)
        {
            if (guid == stats.guid)
            {
                DestroyStateMachine();
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            base.OnDrawGizmos();
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, stats.detectionRadius);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, patrolArea);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, stats.attackRadius);
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, stats.runAwayRadius);
            
            if (weapon != null)
            {
                Gizmos.color = Color.yellow;
                float angle = Vector2.SignedAngle(Vector2.right, transform.right);
                Gizmos.matrix = Matrix4x4.TRS(weapon.collisionPoint.position, Quaternion.Euler(0, 0, angle), Vector3.one);;
                Gizmos.DrawWireCube(Vector3.zero + (Vector3)weapon.collisionPointOffset, Vector2.one * weapon.collisionBox);
            };
        }
    }
}