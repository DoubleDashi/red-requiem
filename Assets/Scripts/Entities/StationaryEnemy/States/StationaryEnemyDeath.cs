using Configs.Events;

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
            StationaryEnemyEventConfig.OnDeath?.Invoke(Controller.stats.guid);
        }

        protected override void SetTransitions()
        {
        }
    }
}