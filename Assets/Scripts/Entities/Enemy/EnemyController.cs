using System;
using Configs.Events;
using FSM;
using Projectiles;
using UnityEngine;
using Utility;

namespace Entities.Enemy
{
    public class EnemyController : StateMachine<EnemyStateType>
    {
        [SerializeField] private EntityStats entityStats;
        [HideInInspector] public bool isHurt;
        
        public EntityStats stats => entityStats;
        public Rigidbody2D body { get; private set; }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            EnemyEventConfig.OnEnemyDeath += HandleOnEnemyDeath;
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            
            EnemyEventConfig.OnEnemyDeath -= HandleOnEnemyDeath;
        }
        
        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            
            InitializeStateMachine(new EnemyStateFactory(this), EnemyStateType.Idle);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(UnityTag.PlayerProjectile.ToString()) && isHurt == false)
            {
                if (other.TryGetComponent(out ShardProjectile shardProjectile))
                {
                    TakeDamage(
                        shardProjectile.config.damage, 
                        shardProjectile.config.enemyKnockbackForce,
                        (transform.position - other.transform.position).normalized
                    );
                }
            }
        }
        
        protected override void SetGlobalTransitions()
        {
            AddGlobalTransition(EnemyStateType.Hurt, () => isHurt);
        }

        public void TakeDamage(float damage, float knockback, Vector2 knockbackDirection)
        {
            isHurt = true;
            EnemyEventConfig.OnEnemyHurt?.Invoke(stats.guid, damage, knockback, knockbackDirection);
        }

        private void HandleOnEnemyDeath(Guid guid)
        {
            if (stats.guid != guid)
            {
                return;
            }

            DestroyStateMachine();
        }
    }
}