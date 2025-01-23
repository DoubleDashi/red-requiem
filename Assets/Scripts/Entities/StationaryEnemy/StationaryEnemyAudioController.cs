using Configs.Audios;
using Controllers;
using UnityEngine;

namespace Entities.StationaryEnemy
{
    public class StationaryEnemyAudioController : AudioController
    {
        [SerializeField] private StationaryEnemyAudioConfig audioConfig;
        
        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }
    }
}