using Configs;

namespace Entities.Enemy.States
{
    public class EnemyDeath : EnemyState
    {
        public EnemyDeath(EnemyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            EnemyEventConfig.OnEnemyDeath?.Invoke(Controller.stats.Guid);
        }

        protected override void SetTransitions()
        {
        }
    }
}