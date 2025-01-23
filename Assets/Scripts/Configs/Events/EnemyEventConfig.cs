﻿using System;
using UnityEngine;

namespace Configs.Events
{
    [CreateAssetMenu(fileName = "EnemyEventConfig", menuName = "Configs/Events/EnemyEventConfig")]
    public class EnemyEventConfig : ScriptableObject
    {
        public static Action<Guid, float, float, Vector2> OnEnemyHurt;
        public static Action<Guid> OnEnemyHurtSFX;
        public static Action<Guid> OnEnemyDeath;
    }
}