using UnityEngine;

namespace Configs.Audios
{
    [CreateAssetMenu(fileName = "MeleeEnemyAudioConfig", menuName = "Configs/Audios/MeleeEnemyAudioConfig")]
    public class MeleeEnemyAudioConfig : ScriptableObject
    {
        public AudioClip hurtSFX;
        public AudioClip deathSFX;
    }
}