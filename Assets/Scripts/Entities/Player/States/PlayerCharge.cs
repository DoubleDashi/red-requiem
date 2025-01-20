using Configs;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerCharge : PlayerState
    {
        private bool _usedSFX;
        private Vector2 _linearVelocity;
        
        public PlayerCharge(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.Stats.currentChargeSpeed = 0f;
        }
        
        public override void Update()
        {
            Accelerate();
            Rotate();

            if (IsCharged() && _usedSFX == false)
            {
                PlayerEventConfig.OnPlayerChargeComplete?.Invoke(Controller.Stats.Guid);
                _usedSFX = true;
            }
            
            Decelerate();
            
            Controller.Body.linearVelocity = _linearVelocity;
        }

        public override void Exit()
        {
            _usedSFX = false;
        }
        
        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => PlayerInput.ChargeCancelKeyPressed);
            AddTransition(PlayerStateType.Attack, () => PlayerInput.ChargeKeyReleased);
        }
        
        private void Accelerate()
        {
            Controller.Stats.currentChargeSpeed += Controller.Stats.chargeSpeed * Time.deltaTime;
            Controller.Stats.currentChargeSpeed = Mathf.Clamp(Controller.Stats.currentChargeSpeed, 0f, Controller.Stats.maxChargeSpeed);
        }

        private void Decelerate()
        {
            if (PlayerInput.MovementDirection.x == 0)
            {
                _linearVelocity.x = Mathf.MoveTowards(_linearVelocity.x, 0.0f, Controller.Stats.decelerationSpeed * Time.deltaTime);  
            }
            
            if (PlayerInput.MovementDirection.y == 0)
            {
                _linearVelocity.y = Mathf.MoveTowards(_linearVelocity.y, 0.0f, Controller.Stats.decelerationSpeed * Time.deltaTime); 
            }
        }
        
        private bool IsCharged()
        {
            return Controller.Stats.currentChargeSpeed == Controller.Stats.maxChargeSpeed;
        }

        private void Rotate()
        {
            Vector2 mousePosition = Controller.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2) Controller.transform.position).normalized;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(0f, 0f, angle);
            
            Controller.transform.rotation = Quaternion.RotateTowards(Controller.transform.rotation, target, Controller.Stats.rotationSpeed * Time.deltaTime);
        }
    }
}