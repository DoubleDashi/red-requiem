using System;
using Entities;
using UnityEngine;

namespace Configs.Events
{
    [CreateAssetMenu(fileName = "PlayerEventConfig", menuName = "Configs/Events/PlayerEventConfig")]
    public class PlayerEventConfig : ScriptableObject
    {
        public static Action<Guid, Damageable> OnHurt;
        public static Action<Guid> OnHurtSFX;
        
        public static Action<Guid> OnDeath;
        public static Action<Guid> OnDeathSFX;

        public static Action<Guid> OnHammerDownSFX;
        public static Action<Guid> OnHammerSwingSFX;
        public static Action<Guid> OnSwordSwingSFX;
        public static Action<Guid> OnShardShootSFX;
        public static Action<Guid, float> OnCannonShootSFX;
    }
}