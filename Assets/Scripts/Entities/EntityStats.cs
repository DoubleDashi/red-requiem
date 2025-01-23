using System;
using TMPro;
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
        public float currentSpeed;
        public float accelerationSpeed;
        public float decelerationSpeed;
        public float brakeSpeed;
        public float maxSpeed;
        public float rotationSpeed;

        [Header("Damage stats")] 
        public float currentDamage;
        public float minDamage;
        public float maxDamage;

        [Header("Charge stats")] 
        public float currentChargeSpeed;
        public float chargeSpeed;
        public float maxChargeSpeed;

        [Header("Misc options")] 
        public bool disableRotation;
    }
}