using Entities.Enemies.MeleeEnemy.States;
using FSM;

namespace Entities.Enemies.MeleeEnemy
{
    public class MeleeEnemyStateFactory : StateFactory<MeleeEnemyStateType>
    {
        private readonly MeleeEnemyController _controller;
        
        public MeleeEnemyStateFactory(MeleeEnemyController controller)
        {
            _controller = controller;
        }

        protected override void SetStates()
        {
            AddState(MeleeEnemyStateType.Idle, new MeleeEnemyIdle(_controller));
            AddState(MeleeEnemyStateType.Patrol, new MeleeEnemyPatrol(_controller));
            AddState(MeleeEnemyStateType.Alert, new MeleeEnemyAlert(_controller));
            AddState(MeleeEnemyStateType.Chase, new MeleeEnemyChase(_controller));
            AddState(MeleeEnemyStateType.Attack, new MeleeEnemyAttack(_controller));
            AddState(MeleeEnemyStateType.AttackWait, new MeleeEnemyAttackWait(_controller));
            AddState(MeleeEnemyStateType.Death, new MeleeEnemyDeath(_controller));
            AddState(MeleeEnemyStateType.Hurt, new MeleeEnemyHurt(_controller));
        }
    }
}