using System;
using Configs.Audios;
using Configs.Events;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Enemies.StationaryEnemy
{
    public class StationaryEnemyAudioController : AudioController
    {
        [SerializeField] private StationaryEnemyAudioConfig audioConfig;
        [SerializeField] private StationaryEnemyController controller;
        
        private void OnEnable()
        {
            StationaryEnemyEventConfig.OnHurtSFX += HandleOnHurtSFX;
            StationaryEnemyEventConfig.OnDeathSFX += HandleOnDeathSFX;
        }

        private void OnDisable()
        {
            StationaryEnemyEventConfig.OnHurtSFX -= HandleOnHurtSFX;
            StationaryEnemyEventConfig.OnDeathSFX -= HandleOnDeathSFX;
        }
        
        private void HandleOnHurtSFX(Guid guid)
        {
            if (guid == controller.stats.guid)
            {
                PlayAudio(audioConfig.hurtSFX, Random.Range(0.9f, 1.1f));    
            }
        }

        private void HandleOnDeathSFX(Guid guid)
        {
            if (guid == controller.stats.guid)
            {
                PlayAudio(audioConfig.deathSFX, Random.Range(0.9f, 1.1f));
            }
        }
    }
}