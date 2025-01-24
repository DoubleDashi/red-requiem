using System;
using Configs.Events;
using Data;
using FSM;
using Projectiles;
using UnityEngine;
using Utility;

namespace Entities.Enemy
{
    public class EnemyController : StateMachine<EnemyStateType>, IEntity
    {
        [SerializeField] private EnemyStats entityStats;
        [HideInInspector] public bool isHurt;
        
        public EnemyStats stats => entityStats;
        public Rigidbody2D body { get; private set; }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            EnemyEventConfig.OnEnemyDeath += HandleOnDeath;
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            
            EnemyEventConfig.OnEnemyDeath -= HandleOnDeath;
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
                    TakeDamage(new Damageable(
                        shardProjectile.config.damage, 
                        shardProjectile.config.enemyKnockbackForce,
                        (transform.position - other.transform.position).normalized
                    ));
                }
            }
        }
        
        protected override void SetGlobalTransitions()
        {
            AddGlobalTransition(EnemyStateType.Hurt, () => isHurt);
        }

        public void TakeDamage(Damageable damageable)
        {
            isHurt = true;
            
            EnemyEventConfig.OnEnemyHurt?.Invoke(stats.guid, damageable);
        }

        public void HandleOnDeath(Guid guid)
        {
            if (stats.guid == guid)
            {
                DestroyStateMachine();
            }
        }
    }
}