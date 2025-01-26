using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class WeaponStats
    {
        [Header("Game objects")]
        public GameObject prefab;
        public Transform pivotPoint;
        public Transform collisionPoint;
        
        [Header("Weapon Stats")]
        public float damage;
        public float enemyKnockbackForce;
        public float selfKnockbackForce;
        public float armorPenetration;
        [Range(0f, 1f)] public float shakeIntensity;
        
        [Header("Collisions")]
        public Vector2 collisionPointOffset;
        public Vector2 collisionBox;

        [Header("Cooldown")]
        public float cooldownTime;
        public bool onCooldown;
    }
}