using System;
using Entities;
using UnityEngine;

namespace Configs.Events
{
    [CreateAssetMenu(fileName = "EnemyEventConfig", menuName = "Configs/Events/EnemyEventConfig")]
    public class EnemyEventConfig : ScriptableObject
    {
        public static Action<Guid, Damageable> OnEnemyHurt;
        public static Action<Guid> OnEnemyHurtSFX;
        public static Action<Guid> OnEnemyDeath;
    }
}