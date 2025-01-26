using UnityEngine;

namespace Configs.Audios
{
    [CreateAssetMenu(fileName = "KitingEnemyAudioConfig", menuName = "Configs/Audios/KitingEnemyAudioConfig")]
    public class KitingEnemyAudioConfig : ScriptableObject
    {
        public AudioClip hurtSFX;
        public AudioClip deathSFX;
    }
}