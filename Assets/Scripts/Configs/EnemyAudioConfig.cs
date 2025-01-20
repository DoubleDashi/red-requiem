using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemyAudioConfig", menuName = "Configs/EnemyAudioConfig")]
    public class EnemyAudioConfig : ScriptableObject
    {
	    public AudioClip hurtSFX;
    }
}