using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public abstract class EntityStats
    {
        public Guid guid { get; private set; } = Guid.NewGuid();

        [Header("Life stats")]
        public float health;
        public float stamina;
    }
}