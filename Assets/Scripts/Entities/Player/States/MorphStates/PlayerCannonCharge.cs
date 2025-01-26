using System.Collections;
using Configs.Events;
using Entities.Player.Factories;
using UnityEngine;

namespace Entities.Player.States.MorphStates
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
            
            Controller.morph.lineRenderer.enabled = true;
            Controller.morph.lineRenderer.SetPosition(0, Vector3.zero);
            Controller.morph.lineRenderer.SetPosition(1, Vector3.zero);
            
            _isComplete = false;
            _length = 0f;
            
            CameraEventConfig.OnConsistentShakeStart?.Invoke(0.25f);
            
            _chargeUpRoutine = Controller.StartCoroutine(ChargeUpRoutine());
        }

        public override void Update()
        {
            Vector3 direction = Quaternion.Euler(0, 0, Controller.morph.pivotPoint.eulerAngles.z) * Vector3.right;
            Controller.morph.lineRenderer.SetPosition(0, Controller.morph.pivotPoint.position - direction * 0.15f);
            Controller.morph.lineRenderer.SetPosition(1, Controller.morph.pivotPoint.position + direction * _length);
            
            Controller.morph.config.collisionPointOffset = new Vector2(_length / 2f, Controller.morph.config.collisionPointOffset.y);
            Controller.morph.config.collisionBox = new Vector2(_length, Controller.morph.config.collisionBox.y);
            
            LineRendererCollisionDetection(direction);
            CollisionDetection();
        }
        
        public override void FixedUpdate()
        {
            Controller.Movement.ForceDecelerate();
            
            Vector2 knockbackDirection = -Controller.morph.pivotPoint.right;
            Controller.body.AddForce(knockbackDirection.normalized * Controller.morph.config.selfKnockbackForce, ForceMode2D.Impulse);
        }

        public override void Exit()
        {
            _length = 0f;
            Controller.morph.lineRenderer.enabled = false;
            
            CameraEventConfig.OnConsistentShakeStop?.Invoke();
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
                _length = Mathf.Lerp(0f, Controller.morph.config.maxLength, elapsed / duration);
                
                yield return null;
            }
            
            _length = Controller.morph.config.maxLength;
            _isComplete = true;
        }

        private void LineRendererCollisionDetection(Vector3 direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                Controller.morph.pivotPoint.position, 
                direction.normalized,
                _length
            );

            if (hit)
            {
                Controller.morph.lineRenderer.SetPosition(1, hit.point);
                Controller.morph.config.collisionPointOffset = new Vector2(hit.distance / 2f, Controller.morph.config.collisionPointOffset.y);
                Controller.morph.config.collisionBox = new Vector2(hit.distance, Controller.morph.config.collisionBox.y);
            }
        }
    }
}