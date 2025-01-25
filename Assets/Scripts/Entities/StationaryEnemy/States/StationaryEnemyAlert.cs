using System.Collections;
using UnityEngine;
using Utility;

namespace Entities.StationaryEnemy.States
{
    public class StationaryEnemyAlert : StationaryEnemyState
    {
        private Coroutine _alertRoutine;
        
        private bool _isComplete;
        private bool _hasDetectedPlayer;
        
        public StationaryEnemyAlert(StationaryEnemyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.spriteRenderer.color = Color.blue;
            
            _isComplete = false;
            
            if (_alertRoutine != null)
            {
                Controller.StopCoroutine(_alertRoutine);
                _alertRoutine = null;
            }
            
            _alertRoutine = Controller.StartCoroutine(AlertRoutine());
        }

        public override void Update()
        {
            _hasDetectedPlayer = DetectCollider(UnityTag.Player);
            CollidersInAggroRange(UnityTag.Player);
            RotateTowardsTarget();
        }

        protected override void SetTransitions()
        {
            AddTransition(StationaryEnemyStateType.Attack, () => _isComplete && _hasDetectedPlayer);
            AddTransition(StationaryEnemyStateType.Idle, () => _isComplete && _hasDetectedPlayer == false);
        }

        private IEnumerator AlertRoutine()
        { 
            yield return new WaitForSeconds(1f);
            
            _isComplete = true;
        }
        
        private void RotateTowardsTarget()
        {
            if (AggroTargetCollider == false)
            {
                return;
            }
            
            Vector2 direction = (AggroTargetCollider.transform.position - Controller.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(0f, 0f, angle);
            
            Controller.transform.rotation = Quaternion.RotateTowards(Controller.transform.rotation, target, Controller.stats.rotationSpeed * Time.deltaTime);
        }
    }
}