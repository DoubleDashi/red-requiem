using Controllers;
using Entities;
using UnityEngine;
using Utility;

namespace Projectiles
{
    public class StationaryEnemyProjectile : ProjectileController
    {
        public float speed;
        public float damage;
        public float knockbackForce;
        
        public void Setup(float parentDamage, float parentKnockbackForce)
        {
            damage = parentDamage;
            knockbackForce = parentKnockbackForce;
        }
        
        protected override void Move()
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        protected override void OnOutbound()
        {
            Destroy(gameObject);    
        }
        
        protected override void CollisionDetection()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                transform.position, 
                transform.localScale.x
                );
            
            foreach (Collider2D other in colliders)
            {
                if (other.CompareTag(UnityTag.Player.ToString()))
                {
                    other.GetComponentInParent<IEntity>().TakeDamage(new Damageable(
                        damage, 
                        knockbackForce,
                        (transform.position - other.transform.position).normalized
                    ));
                    Destroy(gameObject);
                }
            }
        }
    }
}