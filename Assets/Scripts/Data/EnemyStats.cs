using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class EnemyStats : EntityStats
    {
        [Header("Movement stats")] 
        public float movementSpeed;
        public float rotationSpeed;

        [Header("Damage stats")]
        public float damage;
        
        [Header("Collision detection")]
        public float detectionRadius;
        public float attackRadius;
    }
}