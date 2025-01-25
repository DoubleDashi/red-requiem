using System;
using Configs.Audios;
using Configs.Events;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.MeleeEnemy
{
    public class MeleeEnemyAudioController : AudioController
    {
        [SerializeField] private MeleeEnemyAudioConfig audioConfig;
        [SerializeField] private MeleeEnemyController controller;
        
        private void OnEnable()
        {
            MeleeEnemyEventConfig.OnDeathSFX += HandleOnDeathSFX;
            MeleeEnemyEventConfig.OnHurtSFX += HandleOnHurtSFX;
        }
        
        private void OnDisable()
        {
            MeleeEnemyEventConfig.OnDeathSFX -= HandleOnDeathSFX;
            MeleeEnemyEventConfig.OnHurtSFX -= HandleOnHurtSFX;
        }

        private void HandleOnHurtSFX(Guid guid)
        {
            if (guid == controller.stats.guid)
            {
                PlayAudio(audioConfig.hurtSFX, Random.Range(0.5f, 0.75f));
            }
        }

        private void HandleOnDeathSFX(Guid guid)
        {
            if (guid == controller.stats.guid)
            {
                PlayAudio(audioConfig.deathSFX, Random.Range(0.5f, 0.75f));
            }
        }
    }
}