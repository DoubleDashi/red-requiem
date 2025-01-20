using Configs;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerAttack : PlayerState
    {
        private float _decelerationSpeed;
        
        public PlayerAttack(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.EnableDamageHitbox();
            Controller.Body.linearVelocity = Vector2.zero;
            PlayerEventConfig.OnPlayerMove?.Invoke(Controller.Stats.Guid);

            _decelerationSpeed = Controller.Stats.decelerationSpeed;
            Controller.Stats.decelerationSpeed = 15f;
            
            Vector2 mousePosition = Controller.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2) Controller.transform.position).normalized;
            
            Controller.Body.linearVelocity = direction * Controller.Stats.currentChargeSpeed;
        }

        public override void Update()
        {
            Decelerate();
            SetDamage();
        }

        public override void Exit()
        {
            Controller.Stats.decelerationSpeed = _decelerationSpeed;
            
            Controller.DisableDamageHitbox();
        }
        
        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => Controller.Body.linearVelocity == Vector2.zero);
        }
        
        private void Decelerate()
        {
            Vector2 velocity = Controller.Body.linearVelocity;
            
            velocity.x = Mathf.Lerp(velocity.x, 0f, Controller.Stats.decelerationSpeed * Time.deltaTime);
            velocity.y = Mathf.Lerp(velocity.y, 0f, Controller.Stats.decelerationSpeed * Time.deltaTime);
            
            Controller.Body.linearVelocity = velocity;
            
            if (velocity.magnitude < 0.1f)
            {
                Controller.Body.linearVelocity = Vector2.zero;
            }
        }

        private void SetDamage()
        {
            Controller.Stats.currentDamage = Mathf.Lerp(Controller.Stats.minDamage, Controller.Stats.maxDamage, Controller.Body.linearVelocity.magnitude / Controller.Stats.maxChargeSpeed);
        }
    }
}