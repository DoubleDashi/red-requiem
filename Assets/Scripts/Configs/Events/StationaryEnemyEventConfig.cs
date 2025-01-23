using System;
using Entities;
using UnityEngine;

namespace Configs.Events
{
    [CreateAssetMenu(fileName = "StationaryEnemyEventConfig", menuName = "Configs/Events/StationaryEnemyEventConfig")]
    public class StationaryEnemyEventConfig : ScriptableObject
    {
        public static Action<Guid, Damageable> OnHurt;
        public static Action<Guid> OnHurtSFX;

        public static Action<Guid> OnDeath;
        public static Action<Guid> OnDeathSFX;
    }
}