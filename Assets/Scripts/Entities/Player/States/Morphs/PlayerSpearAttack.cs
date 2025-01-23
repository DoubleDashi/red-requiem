using System.Collections;
using Configs;
using Entities.Player.Components;
using UnityEngine;

namespace Entities.Player.States.Morphs
{
    public class PlayerSpearAttack : MorphState
    {
        private float _decelerationSpeed;
        private bool _isComplete;
        private Coroutine _attackEndRoutine;
        
        public PlayerSpearAttack(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            _isComplete = false;
            if (_attackEndRoutine != null)
            {
                Controller.StopCoroutine(_attackEndRoutine);
                _attackEndRoutine = null;
            }
            
            Controller.components.body.linearVelocity = Vector2.zero;
            PlayerEventConfig.OnPlayerMove?.Invoke(Controller.stats.guid);

            _decelerationSpeed = Controller.stats.decelerationSpeed;
            Controller.stats.decelerationSpeed = 15f;
            
            Vector2 mousePosition = Controller.components.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2) Controller.transform.position).normalized;
            
            Controller.components.body.linearVelocity = direction * Controller.stats.currentChargeSpeed;
        }

        public override void Update()
        {
            CollisionDetection();
            SetDamage();
            
            Controller.components.Movement.ForceDecelerate();

            if (Controller.components.body.linearVelocity == Vector2.zero && _attackEndRoutine == null)
            {
                _attackEndRoutine = Controller.StartCoroutine(AttackEndRoutine());
            }
        }

        public override void Exit()
        {
            CollisionClear();
            Controller.stats.decelerationSpeed = _decelerationSpeed;
        }
        
        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => _isComplete);
        }

        private void SetDamage()
        {
            Controller.stats.currentDamage = Mathf.Lerp(Controller.stats.minDamage, Controller.stats.maxDamage, Controller.components.body.linearVelocity.magnitude / Controller.stats.maxChargeSpeed);
        }

        private IEnumerator AttackEndRoutine()
        {
            // Give the player more leeway to hit the enemy
            yield return new WaitForSeconds(0.15f);
            _isComplete = true;
        }
    }
}