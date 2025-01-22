using System;
using Configs;
using Configs.Events;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Enemy
{
    public class EnemyAudio : AudioController
    {
        [SerializeField] private EnemyAudioConfig audioConfig;
        
        private void OnEnable()
        {
            EnemyEventConfig.OnEnemyHurtSFX += HandleOnEnemyHurt;
        }
        
        private void OnDisable()
        {
            EnemyEventConfig.OnEnemyHurtSFX -= HandleOnEnemyHurt;
            
        }

        private void HandleOnEnemyHurt(Guid guid)
        {
            // TODO should probably implement the GUID system here
            
            PlayAudio(audioConfig.hurtSFX, Random.Range(0.5f, 1.5f));
        }
    }
}