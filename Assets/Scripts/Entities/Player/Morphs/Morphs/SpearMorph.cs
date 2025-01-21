using Configs;
using Configs.Morphs;
using Entities.Player.Controllers;
using UnityEngine;
using PlayerInput = Entities.Player.Components.PlayerInput;

namespace Entities.Player.Morphs.Morphs
{
    public class SpearMorph : BaseMorph
    {
        private bool _usedSFX;
        private bool _releasedCharge;
        
        public SpearMorph(PlayerController controller, MorphConfig config) : base(controller, config)
        {
        }

        public override void Enter()
        {
            Controller.components.body.linearVelocity = Vector2.zero;
        }
        
        public override void Update()
        {
            if (PlayerInput.chargeKeyHold && _releasedCharge == false)
            {
                ChargeVelocity();
                if (IsCharged() && _usedSFX == false)
                {
                    PlayerEventConfig.OnPlayerChargeComplete?.Invoke(Controller.stats.guid);
                    _usedSFX = true;
                }    
            }
            
            if (PlayerInput.chargeKeyReleased && _releasedCharge == false)
            {
                _releasedCharge = true;
                PlayerEventConfig.OnPlayerMove?.Invoke(Controller.stats.guid);
            
                Vector2 mousePosition = Controller.components.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (mousePosition - (Vector2) Controller.transform.position).normalized;
            
                Controller.components.body.linearVelocity = direction * Controller.stats.currentChargeSpeed;
            }
            
            if (_releasedCharge)
            {
                Decelerate();
                SetDamage();
            }
        }

        public override void Exit()
        {
            _releasedCharge = false;
            Controller.stats.currentDamage = 0f;
            Controller.stats.currentChargeSpeed = 0f;
        }

        public override bool IsComplete()
        {
            return Controller.components.body.linearVelocity.magnitude == 0f && _releasedCharge;  
        }
        
        private void ChargeVelocity()
        {
            Controller.stats.currentChargeSpeed += Controller.stats.chargeSpeed * Time.deltaTime;
            Controller.stats.currentChargeSpeed = Mathf.Clamp(Controller.stats.currentChargeSpeed, 0f, Controller.stats.maxChargeSpeed);
        }

        private bool IsCharged()
        {
            return Controller.stats.currentChargeSpeed == Controller.stats.maxChargeSpeed;
        }
        
        private void SetDamage()
        {
            Controller.stats.currentDamage = Mathf.Lerp(
                Controller.stats.minDamage, 
                Controller.stats.maxDamage, 
                Controller.components.body.linearVelocity.magnitude / Controller.stats.maxChargeSpeed
            );
        }
        
        private void Decelerate()
        {
            Vector2 velocity = Controller.components.body.linearVelocity;
            
            velocity.x = Mathf.Lerp(velocity.x, 0f, Controller.stats.decelerationSpeed * 3f * Time.deltaTime);
            velocity.y = Mathf.Lerp(velocity.y, 0f, Controller.stats.decelerationSpeed * 3f * Time.deltaTime);
            
            Controller.components.body.linearVelocity = velocity;
            
            if (velocity.magnitude < 0.1f)
            {
                Controller.components.body.linearVelocity = Vector2.zero;
            }
        }
    }
}