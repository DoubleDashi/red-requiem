using UnityEngine;

namespace Configs.Audios
{
    [CreateAssetMenu(fileName = "PlayerAudioConfig", menuName = "Configs/Audios/PlayerAudioConfig")]
    public class PlayerAudioConfig : ScriptableObject
    {
        public AudioClip hurtSFX;
        public AudioClip deathSFX;
        public AudioClip hammerDownSFX;
        public AudioClip hammerSwingSFX;
        public AudioClip swordSwingSFX;
        public AudioClip shardShootSFX;
        public AudioClip cannonShootSFX;
    }
}