using UnityEngine;

namespace Entities
{
    public struct Damageable
    {
        public float Damage;
        
        private readonly float _knockbackForce;
        private readonly Vector2 _knockbackDirection;

        public Vector2 knockback => -_knockbackDirection.normalized * _knockbackForce;
        
        public Damageable(float damage, float knockbackForce, Vector2 knockbackDirection)
        {
            Damage = damage;
            
            _knockbackForce = knockbackForce;
            _knockbackDirection = knockbackDirection;
        }
    }
}