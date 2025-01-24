using UnityEngine;

namespace Configs.Audios
{
    [CreateAssetMenu(fileName = "StationaryEnemyAudioConfig", menuName = "Configs/Audios/StationaryEnemyAudioConfig")]
    public class StationaryEnemyAudioConfig : ScriptableObject
    {
        public AudioClip hurtSFX;
    }
}