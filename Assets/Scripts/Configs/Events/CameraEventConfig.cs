using System;
using UnityEngine;

namespace Configs.Events
{
    [CreateAssetMenu(fileName = "CameraEventConfig", menuName = "Configs/Events/Camera Event Config")]
    public class CameraEventConfig : ScriptableObject
    {
        public static Action<float> OnShake;
        public static Action<float> OnConsistentShakeStart;
        public static Action OnConsistentShakeStop;
    }
}