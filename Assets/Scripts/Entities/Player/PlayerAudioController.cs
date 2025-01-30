using System;
using Configs.Audios;
using Configs.Events;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Player
{
    public class PlayerAudioController : AudioController
    {
        [SerializeField] private PlayerAudioConfig audioConfig;
        
        private void OnEnable()
        {
            PlayerEventConfig.OnHurtSFX += HandleOnHurtSFX;
            PlayerEventConfig.OnDeathSFX += HandleOnDeathSFX;
            PlayerEventConfig.OnHammerDownSFX += HandleOnHammerDownSFX;
            PlayerEventConfig.OnHammerSwingSFX += HandleOnHammerSwingSFX;
            PlayerEventConfig.OnSwordSwingSFX += HandleOnSwordSwingSFX;
            PlayerEventConfig.OnShardShootSFX += HandleOnShardShootSFX;
            PlayerEventConfig.OnCannonShootSFX += HandleOnCannonShootSFX;
        }
        
        private void OnDisable()
        {
            PlayerEventConfig.OnHurtSFX -= HandleOnHurtSFX;
            PlayerEventConfig.OnDeathSFX -= HandleOnDeathSFX;
            PlayerEventConfig.OnHammerDownSFX -= HandleOnHammerDownSFX;
            PlayerEventConfig.OnHammerSwingSFX -= HandleOnHammerSwingSFX;
            PlayerEventConfig.OnSwordSwingSFX -= HandleOnSwordSwingSFX;
            PlayerEventConfig.OnShardShootSFX -= HandleOnShardShootSFX;
            PlayerEventConfig.OnCannonShootSFX -= HandleOnCannonShootSFX;
        }
        
        private void HandleOnHurtSFX(Guid guid)
        {
            PlayAudio(audioConfig.hurtSFX, Random.Range(0.5f, 0.75f));
        }

        private void HandleOnDeathSFX(Guid guid)
        {
            PlayAudio(audioConfig.deathSFX, Random.Range(0.5f, 0.75f));
        }

        private void HandleOnHammerDownSFX(Guid guid)
        {
            PlayAudio(audioConfig.hammerDownSFX, Random.Range(0.8f, 1.2f));
        }
        
        private void HandleOnHammerSwingSFX(Guid guid)
        {
            PlayAudio(audioConfig.hammerSwingSFX, Random.Range(0.8f, 1.2f));
        }
        
        private void HandleOnSwordSwingSFX(Guid guid)
        {
            PlayAudio(audioConfig.swordSwingSFX, Random.Range(0.8f, 1.2f));
        }
        
        private void HandleOnShardShootSFX(Guid guid)
        {
            PlayAudio(audioConfig.shardShootSFX, Random.Range(0.8f, 1.2f));
        }

        private void HandleOnCannonShootSFX(Guid guid, float volume)
        {
            PlayAudio(audioConfig.cannonShootSFX, Random.Range(0.8f, 1.2f), volume);
        }
    }
}