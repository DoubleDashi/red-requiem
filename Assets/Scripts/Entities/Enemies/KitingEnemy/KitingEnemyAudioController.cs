using System;
using Configs.Audios;
using Configs.Events;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Enemies.KitingEnemy
{
    public class KitingEnemyAudioController : AudioController
    {
        [SerializeField] private KitingEnemyAudioConfig audioConfig;
        [SerializeField] private KitingEnemyController controller;
        
        private void OnEnable()
        {
            KitingEnemyEventConfig.OnDeathSFX += HandleOnDeathSFX;
            KitingEnemyEventConfig.OnHurtSFX += HandleOnHurtSFX;
        }
        
        private void OnDisable()
        {
            KitingEnemyEventConfig.OnDeathSFX -= HandleOnDeathSFX;
            KitingEnemyEventConfig.OnHurtSFX -= HandleOnHurtSFX;
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