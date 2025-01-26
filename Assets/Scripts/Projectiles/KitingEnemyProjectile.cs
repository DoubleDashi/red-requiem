using Controllers;
using Entities;
using UnityEngine;
using Utility;

namespace Projectiles
{
    public class KitingEnemyProjectile : ProjectileController
    {
        public float speed;
        [HideInInspector] public float damage;
        [HideInInspector] public float knockbackForce;
        [HideInInspector] public float shakeIntensity;
        [HideInInspector] public float armorPenetration;
        
        public void Setup(float parentDamage, float parentKnockbackForce, float parentShakeIntensity, float parentArmorPenetration)
        {
            damage = parentDamage;
            knockbackForce = parentKnockbackForce;
            shakeIntensity = parentShakeIntensity;
            armorPenetration = parentArmorPenetration;
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
                        (transform.position - other.transform.position).normalized,
                        shakeIntensity,
                        armorPenetration
                    ));
                    Destroy(gameObject);
                }
            }
        }
    }
}