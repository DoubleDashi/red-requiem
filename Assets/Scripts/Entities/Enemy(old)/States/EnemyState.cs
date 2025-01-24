using FSM;

namespace Entities.Enemy.States
{
    public abstract class EnemyState : BaseState<EnemyStateType>
    {
        protected readonly EnemyController Controller;
        
        protected EnemyState(EnemyController controller)
        {
            Controller = controller;
        }
    }
}