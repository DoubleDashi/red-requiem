using Entities.StationaryEnemy.States;
using FSM;

namespace Entities.StationaryEnemy
{
    public class StationaryEnemyStateFactory : StateFactory<StationaryEnemyStateType>
    {
        private readonly StationaryEnemyController _controller;
        
        public StationaryEnemyStateFactory(StationaryEnemyController controller)
        {
            _controller = controller;
        }
        
        protected override void SetStates()
        {
            AddState(StationaryEnemyStateType.Idle, new StationaryEnemyIdle(_controller));
            AddState(StationaryEnemyStateType.Alert, new StationaryEnemyAlert(_controller));
            AddState(StationaryEnemyStateType.Attack, new StationaryEnemyAttack(_controller));
            
            AddState(StationaryEnemyStateType.Hurt, new StationaryEnemyHurt(_controller));
            AddState(StationaryEnemyStateType.Death, new StationaryEnemyDeath(_controller));
        }
    }
}