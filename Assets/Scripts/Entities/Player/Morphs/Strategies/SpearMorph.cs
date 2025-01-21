using Configs;
using UnityEngine;

namespace Entities.Player.Morphs.Strategies
{
    public class SpearMorph : BaseMorph
    {
        private float _decelerationSpeed;
        
        public SpearMorph(PlayerController controller, MorphDTO morphDTO) : base(controller, morphDTO)
        {
            HasCharge = true;
        }

        public override void Attack()
        {
            Controller.EnableDamageHitbox();
            Controller.components.Body.linearVelocity = Vector2.zero;
            PlayerEventConfig.OnPlayerMove?.Invoke(Controller.stats.Guid);

            _decelerationSpeed = Controller.stats.decelerationSpeed;
            Controller.stats.decelerationSpeed = 15f;
            
            Vector2 mousePosition = Controller.components.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2) Controller.transform.position).normalized;
            
            Controller.components.Body.linearVelocity = direction * Controller.stats.currentChargeSpeed;
        }

        public void Update()
        {
            SetDamage();
            Decelerate();
        }

        public void Exit()
        {
            Controller.stats.decelerationSpeed = _decelerationSpeed;
            Controller.DisableDamageHitbox();
        }

        public override bool IsFinished()
        {
            return Controller.components.Body.linearVelocity == Vector2.zero;
        }
        
        public void ChargeVelocity()
        {
            Controller.stats.currentChargeSpeed += Controller.stats.chargeSpeed * Time.deltaTime;
            Controller.stats.currentChargeSpeed = Mathf.Clamp(Controller.stats.currentChargeSpeed, 0f, Controller.stats.maxChargeSpeed);
        }

        public bool IsCharged()
        {
            return Controller.stats.currentChargeSpeed == Controller.stats.maxChargeSpeed;
        }
        
        private void SetDamage()
        {
            Controller.stats.currentDamage = Mathf.Lerp(
                Controller.stats.minDamage, 
                Controller.stats.maxDamage, 
                Controller.components.Body.linearVelocity.magnitude / Controller.stats.maxChargeSpeed
            );
        }
        
        private void Decelerate()
        {
            Vector2 velocity = Controller.components.Body.linearVelocity;
            
            velocity.x = Mathf.Lerp(velocity.x, 0f, Controller.stats.decelerationSpeed * Time.deltaTime);
            velocity.y = Mathf.Lerp(velocity.y, 0f, Controller.stats.decelerationSpeed * Time.deltaTime);
            
            Controller.components.Body.linearVelocity = velocity;
            
            if (velocity.magnitude < 0.1f)
            {
                Controller.components.Body.linearVelocity = Vector2.zero;
            }
        }
    }
}