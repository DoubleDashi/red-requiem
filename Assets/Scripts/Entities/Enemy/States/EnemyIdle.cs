namespace Entities.Enemy.States
{
    public class EnemyIdle : EnemyState
    {
        public EnemyIdle(EnemyController controller) : base(controller)
        {
        }

        protected override void SetTransitions()
        {
            AddTransition(EnemyStateType.Hurt, () => Controller.isHurt);
        }
    }
}