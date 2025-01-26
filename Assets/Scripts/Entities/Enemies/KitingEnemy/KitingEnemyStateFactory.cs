using Entities.Enemies.KitingEnemy.States;
using FSM;

namespace Entities.Enemies.KitingEnemy
{
    public class KitingEnemyStateFactory : StateFactory<KitingEnemyStateType>
    {
        private readonly KitingEnemyController _controller;
        
        public KitingEnemyStateFactory(KitingEnemyController controller)
        {
            _controller = controller;
        }
        
        protected override void SetStates()
        {
            AddState(KitingEnemyStateType.Idle, new KitingEnemyIdle(_controller));
            AddState(KitingEnemyStateType.Patrol, new KitingEnemyPatrol(_controller));
            AddState(KitingEnemyStateType.Alert, new KitingEnemyAlert(_controller));
            AddState(KitingEnemyStateType.Chase, new KitingEnemyChase(_controller));
            AddState(KitingEnemyStateType.Attack, new KitingEnemyAttack(_controller));
            AddState(KitingEnemyStateType.AttackWait, new KitingEnemyAttackWait(_controller));
            AddState(KitingEnemyStateType.RunAway, new KitingEnemyRunAway(_controller));
            AddState(KitingEnemyStateType.Death, new KitingEnemyDeath(_controller));
            AddState(KitingEnemyStateType.Hurt, new KitingEnemyHurt(_controller));
        }
    }
}