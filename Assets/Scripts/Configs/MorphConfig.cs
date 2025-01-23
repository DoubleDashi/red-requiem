﻿using Entities.Player.Components;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MorphConfig", menuName = "Configs/MorphConfig", order = 0)]
    public class MorphConfig : ScriptableObject
    {
        [Header("General")]
        public MorphType type;
        public GameObject prefab;
        public float enemyKnockbackForce;
        public float selfKnockbackForce;
        public Vector2 collisionPointOffset;
        public Vector2 collisionBox;
        public float damage;
        
        [Header("Projectile specific")]
        public float speed;
        
        [Header("Cannon specific")]
        public bool hasFireRate;
        public float fireRate;
        public float maxLength;
    }
}