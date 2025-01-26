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
        }
        
        private void OnDisable()
        {
            PlayerEventConfig.OnHurtSFX -= HandleOnHurtSFX;
            PlayerEventConfig.OnDeathSFX -= HandleOnDeathSFX;
            PlayerEventConfig.OnHammerDownSFX -= HandleOnHammerDownSFX;
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
    }
}