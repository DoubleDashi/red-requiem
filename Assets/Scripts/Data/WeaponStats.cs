using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class WeaponStats
    {
        public GameObject prefab;
        public Transform pivotPoint;
        public Transform collisionPoint;
        
        public float damage;
        public float enemyKnockbackForce;
        public float selfKnockbackForce;
        
        public Vector2 collisionPointOffset;
        public Vector2 collisionBox;

        public float cooldownTime;
        public bool onCooldown;
    }
}