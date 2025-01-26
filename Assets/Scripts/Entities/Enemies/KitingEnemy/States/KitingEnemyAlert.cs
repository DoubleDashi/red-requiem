using System.Collections;
using UnityEngine;
using Utility;

namespace Entities.Enemies.KitingEnemy.States
{
    public class KitingEnemyAlert : KitingEnemyState
    {
        private bool _isComplete;
        private bool _inAggroRange;
        private bool _inAttackRange;
        private bool _inRunAwayRange;
        
        public KitingEnemyAlert(KitingEnemyController controller) : base(controller)
        {
        }
        
        public override void Enter()
        {
            _isComplete = false;
            
            Controller.spriteRenderer.color = Color.magenta;
            Controller.StartCoroutine(AlertRoutine());
        }
        
        public override void Update()
        {
            _inAggroRange = CollidersInAggroRange(UnityTag.Player);
            _inAttackRange = CollidersInAttackRange(UnityTag.Player);
            _inRunAwayRange = CollidersInRunAwayRange(UnityTag.Player);
        }

        protected override void SetTransitions()
        {
            AddTransition(KitingEnemyStateType.RunAway, () => _isComplete && _inRunAwayRange);
            AddTransition(KitingEnemyStateType.Attack, () => _isComplete && _inAttackRange && Controller.weapon.onCooldown == false);
            AddTransition(KitingEnemyStateType.Chase, () => _isComplete && _inAggroRange);
            AddTransition(KitingEnemyStateType.Idle, () => _isComplete && _inAggroRange == false);
        }
        
        private IEnumerator AlertRoutine()
        {
            yield return new WaitForSeconds(0.25f);
            _isComplete = true;
        }
    }
}