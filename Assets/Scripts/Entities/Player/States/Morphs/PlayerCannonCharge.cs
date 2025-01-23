using System.Collections;
using Entities.Player.Components;
using UnityEngine;

namespace Entities.Player.States.Morphs
{
    public class PlayerCannonCharge : MorphState
    {
        private bool _isComplete;
        private float _length;
        private Coroutine _chargeUpRoutine;
        
        public PlayerCannonCharge(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            if (_chargeUpRoutine != null)
            {
                Controller.StopCoroutine(_chargeUpRoutine);
            }
            
            Controller.cannonLine.enabled = true;
            Controller.cannonLine.SetPosition(0, Vector3.zero);
            Controller.cannonLine.SetPosition(1, Vector3.zero);
            
            _isComplete = false;
            _length = 0f;
            
            _chargeUpRoutine = Controller.StartCoroutine(ChargeUpRoutine());
        }

        public override void Update()
        {
            Vector3 direction = Quaternion.Euler(0, 0, Controller.weaponPivot.eulerAngles.z) * Vector3.right;
            Controller.cannonLine.SetPosition(0, Controller.weaponPivot.position - direction * 0.15f);
            Controller.cannonLine.SetPosition(1, Controller.weaponPivot.position + direction * _length);
            
            CollisionDetection();
        }
        
        public override void FixedUpdate()
        {
            Controller.components.Movement.ForceDecelerate();
            
            Vector2 knockbackDirection = -Controller.weaponPivot.right;
            Controller.components.body.AddForce(knockbackDirection.normalized * Controller.currentMorph.selfKnockbackForce, ForceMode2D.Impulse);
        }

        public override void Exit()
        {
            _length = 0f;
            Controller.cannonLine.enabled = false;
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.CannonAttack, () => _isComplete);
            AddTransition(PlayerStateType.Idle, () => Input.GetKey(KeyCode.Mouse0) == false);
        }

        private IEnumerator ChargeUpRoutine()
        {
            const float duration = 0.5f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                _length = Mathf.Lerp(0f, Controller.currentMorph.maxLength, elapsed / duration);
                Controller.currentMorph.collisionPointOffset = new Vector2(_length / 2f, Controller.currentMorph.collisionPointOffset.y);
                Controller.currentMorph.collisionBox = new Vector2(_length, Controller.currentMorph.collisionBox.y);
                
                yield return null;
            }
            
            _length = Controller.currentMorph.maxLength;
            _isComplete = true;
        }
    }
}