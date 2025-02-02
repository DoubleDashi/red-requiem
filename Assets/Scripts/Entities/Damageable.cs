﻿using UnityEngine;

namespace Entities
{
    public readonly struct Damageable
    {
        public readonly float Damage;
        public readonly float ShakeIntensity;
        public readonly float ArmorPenetration;
        
        private readonly float _knockbackForce;
        private readonly Vector2 _knockbackDirection;

        public Vector2 knockback => -_knockbackDirection.normalized * _knockbackForce;
        
        public Damageable(float damage, float knockbackForce, Vector2 knockbackDirection, float shakeIntensity, float armorPenetration)
        {
            Damage = damage;
            
            _knockbackForce = knockbackForce;
            _knockbackDirection = knockbackDirection;
            ShakeIntensity = shakeIntensity;
            ArmorPenetration = armorPenetration;
        }
    }
}