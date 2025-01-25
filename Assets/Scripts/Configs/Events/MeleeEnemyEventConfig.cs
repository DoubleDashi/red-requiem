using System;
using Entities;
using UnityEngine;

namespace Configs.Events
{
    [CreateAssetMenu(fileName = "MeleeEnemyEventConfig", menuName = "Configs/Events/MeleeEnemyEventConfig")]
    public class MeleeEnemyEventConfig : ScriptableObject
    {
        public static Action<Guid, Damageable> OnHurt;
        public static Action<Guid> OnHurtSFX;

        public static Action<Guid> OnDeath;
        public static Action<Guid> OnDeathSFX;
    }
}