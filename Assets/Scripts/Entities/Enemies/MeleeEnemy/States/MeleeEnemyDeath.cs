using System.Collections;
using Configs.Events;
using UnityEngine;

namespace Entities.Enemies.MeleeEnemy.States
{
    public class MeleeEnemyDeath : MeleeEnemyState
    {
        private bool _hasDetectedPlayer;
        
        public MeleeEnemyDeath(MeleeEnemyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            MeleeEnemyEventConfig.OnDeathSFX?.Invoke(Controller.stats.guid);
            
            Controller.StartCoroutine(DeathRoutine());
        }

        protected override void SetTransitions()
        {
        }
        
        private IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(0.2f);
            
            MeleeEnemyEventConfig.OnDeath?.Invoke(Controller.stats.guid);
        }
    }
}