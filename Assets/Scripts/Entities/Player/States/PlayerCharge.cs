using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerCharge : PlayerState
    {
        public PlayerCharge(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.Stats.currentSpeed = 0f;
        }
        
        public override void Update()
        {
            Accelerate();
            Rotate();
        }
        
        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => PlayerInput.ChargeCancelKeyPressed);
            AddTransition(PlayerStateType.Move, () => PlayerInput.ChargeKeyReleased);
        }
        
        private void Accelerate()
        {
            Controller.Stats.currentSpeed += Controller.Stats.accelerationSpeed * Time.deltaTime;
            Controller.Stats.currentSpeed = Mathf.Clamp(Controller.Stats.currentSpeed, 0f, Controller.Stats.maxSpeed);
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