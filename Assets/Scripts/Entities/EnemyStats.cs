using System;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public class EnemyStats
    {
        public Guid guid { get; private set; } = Guid.NewGuid();
        
        [Header("Life stats")] 
        public float health;
        public float stamina;

        [Header("Movement stats")] 
        public float movementSpeed;
        public float rotationSpeed;

        [Header("Damage stats")]
        public float damage;
        
        [Header("Collision detection")]
        public float detectionRadius;
    }
}