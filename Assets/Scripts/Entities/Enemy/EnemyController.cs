using System;
using Configs;
using Controllers;
using Entities.Player;
using FSM;
using UnityEngine;
using Utility;

namespace Entities.Enemy
{
    public class EnemyController : StateMachine<EnemyStateType>
    {
        [SerializeField] private EntityStats entityStats;
        
        [HideInInspector] public bool isHurt;
        
        public EntityStats stats => entityStats;
        
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
            InitializeStateMachine(new EnemyStateFactory(this), EnemyStateType.Idle);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(UnityTag.PlayerDamageHitbox.ToString()) && isHurt == false)
            {
                isHurt = true;
                EnemyEventConfig.OnEnemyHurt?.Invoke(stats.Guid, other.GetComponentInParent<PlayerController>().stats.currentDamage);
            }
            
            if (other.CompareTag(UnityTag.PlayerProjectile.ToString()) && isHurt == false)
            {
                isHurt = true;
                EnemyEventConfig.OnEnemyHurt?.Invoke(stats.Guid, other.GetComponent<ProjectileController>().stats.damage);
            }
        }
        
        protected override void SetGlobalTransitions()
        {
            AddGlobalTransition(EnemyStateType.Hurt, () => isHurt);
        }

        private void HandleOnEnemyDeath(Guid guid)
        {
            if (stats.Guid != guid)
            {
                return;
            }

            DestroyStateMachine();
        }
    }
}