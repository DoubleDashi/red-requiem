using System;
using Configs.Events;
using FSM;
using UnityEngine;

namespace Entities.StationaryEnemy
{
    public class StationaryEnemyController : StateMachine<StationaryEnemyStateType>, IEntity
    {
        public EnemyStats stats;
        
        [HideInInspector] public SpriteRenderer spriteRenderer;
        [HideInInspector] public Rigidbody2D body;
        [HideInInspector] public bool isHurt;

        protected override void Subscribe()
        {
            StationaryEnemyEventConfig.OnDeath += HandleOnDeath;
        }
        
        protected override void Unsubscribe()
        {
            StationaryEnemyEventConfig.OnDeath -= HandleOnDeath;
        }
        
        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            body = GetComponent<Rigidbody2D>();
            
            InitializeStateMachine(
                new StationaryEnemyStateFactory(this),
                StationaryEnemyStateType.Idle
            );
        }
        
        protected override void SetGlobalTransitions()
        {
            AddGlobalTransition(StationaryEnemyStateType.Hurt, () => isHurt);
        }

        public void TakeDamage(Damageable damageable)
        {
            isHurt = true;
            
            StationaryEnemyEventConfig.OnHurt?.Invoke(stats.guid, damageable);
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
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, stats.detectionRadius);
        }
    }
}