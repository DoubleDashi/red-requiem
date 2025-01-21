using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemyAudioConfig", menuName = "Configs/Audios/EnemyAudioConfig")]
    public class EnemyAudioConfig : ScriptableObject
    {
	    public AudioClip hurtSFX;
    }
}