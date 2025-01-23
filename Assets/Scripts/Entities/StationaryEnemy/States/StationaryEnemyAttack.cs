using UnityEngine;
using Utility;

namespace Entities.StationaryEnemy.States
{
    public class StationaryEnemyAttack : StationaryEnemyState
    {
        private bool _hasDetectedPlayer;
        
        public StationaryEnemyAttack(StationaryEnemyController controller) : base(controller)
        {
        }
        
        public override void Enter()
        {
            Controller.spriteRenderer.color = Color.magenta;
        }

        public override void Update()
        {
            _hasDetectedPlayer = DetectCollider(UnityTag.Player);
        }

        protected override void SetTransitions()
        {
            AddTransition(StationaryEnemyStateType.Idle, () => _hasDetectedPlayer == false);
        }
    }
}