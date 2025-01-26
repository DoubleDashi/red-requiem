using System.Collections;
using Configs.Events;
using UnityEngine;

namespace Entities.Enemies.KitingEnemy.States
{
    public class KitingEnemyDeath : KitingEnemyState
    {
        private bool _hasDetectedPlayer;
        
        public KitingEnemyDeath(KitingEnemyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            KitingEnemyEventConfig.OnDeathSFX?.Invoke(Controller.stats.guid);
            
            Controller.StartCoroutine(DeathRoutine());
        }

        protected override void SetTransitions()
        {
        }
        
        private IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(0.2f);
            
            KitingEnemyEventConfig.OnDeath?.Invoke(Controller.stats.guid);
        }
    }
}