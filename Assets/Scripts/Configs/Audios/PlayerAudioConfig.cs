using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerAudioConfig", menuName = "Configs/Audios/PlayerAudioConfig")]
    public class PlayerAudioConfig : ScriptableObject
    {
        public AudioClip[] chargeCompleteSFX;
        public AudioClip[] moveSFX;
    }
}