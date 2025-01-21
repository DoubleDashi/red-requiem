using Configs;
using Entities.Player.Components;
using Entities.Player.Controllers;
using UnityEngine;

namespace Entities.Player.States.Morphs
{
    public class PlayerSpearCharge : MorphState
    {
        private bool _usedSFX;
        private Vector2 _linearVelocity;
        
        public PlayerSpearCharge(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.stats.currentChargeSpeed = 0f;
        }
        
        public override void Update()
        {
            Accelerate();
            Rotate();

            if (IsCharged() && _usedSFX == false)
            {
                PlayerEventConfig.OnPlayerChargeComplete?.Invoke(Controller.stats.guid);
                _usedSFX = true;
            }
            
            Decelerate();
            
            Controller.components.body.linearVelocity = _linearVelocity;
        }

        public override void Exit()
        {
            _usedSFX = false;
        }
        
        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => PlayerInput.chargeCancelKeyPressed);
            AddTransition(PlayerStateType.SpearAttack, () => PlayerInput.chargeKeyReleased);
        }
        
        private void Accelerate()
        {
            Controller.stats.currentChargeSpeed += Controller.stats.chargeSpeed * Time.deltaTime;
            Controller.stats.currentChargeSpeed = Mathf.Clamp(Controller.stats.currentChargeSpeed, 0f, Controller.stats.maxChargeSpeed);
        }

        private void Decelerate()
        {
            if (PlayerInput.movementDirection.x == 0)
            {
                _linearVelocity.x = Mathf.MoveTowards(_linearVelocity.x, 0.0f, Controller.stats.decelerationSpeed * Time.deltaTime);  
            }
            
            if (PlayerInput.movementDirection.y == 0)
            {
                _linearVelocity.y = Mathf.MoveTowards(_linearVelocity.y, 0.0f, Controller.stats.decelerationSpeed * Time.deltaTime); 
            }
        }
        
        private bool IsCharged()
        {
            return Controller.stats.currentChargeSpeed == Controller.stats.maxChargeSpeed;
        }

        private void Rotate()
        {
            Vector2 mousePosition = Controller.components.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2) Controller.transform.position).normalized;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(0f, 0f, angle);
            
            Controller.transform.rotation = Quaternion.RotateTowards(Controller.transform.rotation, target, Controller.stats.rotationSpeed * Time.deltaTime);
        }
    }
}