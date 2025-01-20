using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerAudioConfig", menuName = "Configs/PlayerAudioConfig")]
    public class PlayerAudioConfig : ScriptableObject
    {
        public AudioClip[] chargeCompleteSFX;
        public AudioClip[] moveSFX;
    }
}