using System;
using Entities;
using UnityEngine;

namespace Configs.Events
{
    [CreateAssetMenu(fileName = "FloatingCombatTextEventConfig", menuName = "Configs/Events/FloatingCombatTextEventConfig")]
    public class FloatingCombatTextEventConfig : ScriptableObject
    {
        public static Action<EntityController, float> OnHurt;
    }
}