using System;
using Entities;
using UnityEngine;

namespace Configs.Events
{
    [CreateAssetMenu(fileName = "KitingEnemyEventConfig", menuName = "Configs/Events/KitingEnemyEventConfig")]
    public class KitingEnemyEventConfig : ScriptableObject
    {
        public static Action<Guid, Damageable> OnHurt;
        public static Action<Guid> OnHurtSFX;

        public static Action<Guid> OnDeath;
        public static Action<Guid> OnDeathSFX;
    }
}