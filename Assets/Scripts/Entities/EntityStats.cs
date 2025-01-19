using System;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public class EntityStats
    {
        public Guid guid { get; private set; } = Guid.NewGuid();

        [Header("Life stats")]
        public float health;
        public float stamina;

        [Header("Movement stats")]
        public float accelerationSpeed;
        public float decelerationSpeed;
        public float maxSpeed;
    }
}