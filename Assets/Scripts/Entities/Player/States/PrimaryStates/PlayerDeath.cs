using System.Collections;
using Configs.Events;
using UnityEngine;

namespace Entities.Player.States.PrimaryStates
{
    public class PlayerDeath : PlayerState
    {
        private bool _hasDetectedPlayer;
        
        public PlayerDeath(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            PlayerEventConfig.OnDeathSFX?.Invoke(Controller.stats.guid);
            
            Controller.StartCoroutine(DeathRoutine());
        }

        protected override void SetTransitions()
        {
        }
        
        private IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(0.2f);
            
            PlayerEventConfig.OnDeath?.Invoke(Controller.stats.guid);
        }
    }
}