using Configs;
using Controllers;
using UnityEngine;

namespace Entities.Enemy
{
    public class EnemyAudio : AudioController
    {
        [SerializeField] private EnemyAudioConfig audioConfig;
        
        private void OnEnable()
        {
            EnemyEventConfig.OnEnemyHurt += HandleOnEnemyHurt;
        }
        
        private void OnDisable()
        {
            EnemyEventConfig.OnEnemyHurt -= HandleOnEnemyHurt;
            
        }

        private void HandleOnEnemyHurt()
        {
            PlayAudio(audioConfig.hurtSFX, Random.Range(0.5f, 1.5f));
        }
    }
}