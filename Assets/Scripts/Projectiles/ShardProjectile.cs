using Configs;
using Controllers;
using Entities;
using UnityEngine;
using Utility;

namespace Projectiles
{
    public class ShardProjectile : ProjectileController
    {
        public MorphConfig config;
        
        protected override void Move()
        {
            transform.position += transform.right * config.speed * Time.deltaTime;
        }

        protected override void OnOutbound()
        {
            ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
            particles.Stop();

            if (particles.IsAlive() == false)
            {
                Destroy(gameObject);    
            }
        }

        protected override void CollisionDetection()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                transform.position, 
                transform.localScale.x
                );
            
            foreach (Collider2D other in colliders)
            {
                if (other.CompareTag(UnityTag.Enemy.ToString()))
                {
                    other.GetComponentInParent<IEntity>().TakeDamage(new Damageable(
                        config.damage, 
                        config.enemyKnockbackForce,
                        (transform.position - other.transform.position).normalized,
                        config.shakeIntensity,
                        config.armorPenetration
                    ));
                    Destroy(gameObject);
                }
            }
        }
    }
}