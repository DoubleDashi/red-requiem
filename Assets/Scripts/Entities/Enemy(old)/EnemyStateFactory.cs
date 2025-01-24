using Entities.Enemy.States;
using FSM;

namespace Entities.Enemy
{
    public class EnemyStateFactory : StateFactory<EnemyStateType>
    {
        private readonly EnemyController _controller;
        
        public EnemyStateFactory(EnemyController controller)
        {
            _controller = controller;
        }
        
        protected override void SetStates()
        {
            AddState(EnemyStateType.Idle, new EnemyIdle(_controller));
            AddState(EnemyStateType.Hurt, new EnemyHurt(_controller));
            AddState(EnemyStateType.Death, new EnemyDeath(_controller));
            AddState(EnemyStateType.Move, new EnemyMove(_controller));
        }
    }
}