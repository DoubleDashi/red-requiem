using System.Collections;
using Configs.Events;
using UnityEngine;

namespace Entities.StationaryEnemy.States
{
    public class StationaryEnemyDeath : StationaryEnemyState
    {
        private bool _hasDetectedPlayer;
        
        public StationaryEnemyDeath(StationaryEnemyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            StationaryEnemyEventConfig.OnDeathSFX?.Invoke(Controller.stats.guid);
            
            Controller.StartCoroutine(DeathRoutine());
        }

        protected override void SetTransitions()
        {
        }
        
        private IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(0.2f);
            
            StationaryEnemyEventConfig.OnDeath?.Invoke(Controller.stats.guid);
        }
    }
}