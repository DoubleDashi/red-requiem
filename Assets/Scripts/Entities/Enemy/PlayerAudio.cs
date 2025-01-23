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

        private EnemyController _controller;
        
        private void OnEnable()
        {
            EnemyEventConfig.OnEnemyHurtSFX += HandleOnEnemyHurt;
        }
        
        private void OnDisable()
        {
            EnemyEventConfig.OnEnemyHurtSFX -= HandleOnEnemyHurt;
        }

        private void Awake()
        {
            _controller = GetComponent<EnemyController>();
        }

        private void HandleOnEnemyHurt(Guid guid)
        {
            if (_controller.stats.guid != guid)
            {
                return;
            }
            
            PlayAudio(audioConfig.hurtSFX, Random.Range(0.5f, 1.5f));
        }
    }
}