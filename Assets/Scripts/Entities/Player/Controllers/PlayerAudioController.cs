using System;
using Configs;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Player.Controllers
{
    public class PlayerAudioController : AudioController
    {
        [SerializeField] private PlayerAudioConfig audioConfig;
        
        private void OnEnable()
        {
            PlayerEventConfig.OnPlayerChargeComplete += HandleOnPlayerChargeComplete;
            PlayerEventConfig.OnPlayerMove += HandleOnPlayerMove;
        }
        
        private void OnDisable()
        {
            PlayerEventConfig.OnPlayerChargeComplete -= HandleOnPlayerChargeComplete;
            PlayerEventConfig.OnPlayerMove -= HandleOnPlayerMove;
        }
        
        private void HandleOnPlayerChargeComplete(Guid playerId)
        {
            PlayAudio(audioConfig.chargeCompleteSFX, Random.Range(0.7f, 1.3f));
        }
        
        private void HandleOnPlayerMove(Guid playerId)
        {
            PlayAudio(audioConfig.moveSFX, Random.Range(0.7f, 1.3f));
        }
    }
}