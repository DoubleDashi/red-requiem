using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerMove : PlayerState
    {
        private Vector2 _linearVelocity;
        
        public PlayerMove(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            _linearVelocity = Controller.Body.linearVelocity;
        }

        public override void Update()
        {
            Accelerate();
            Decelerate();
            Brake();
            
            Controller.Body.linearVelocity = _linearVelocity;
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => PlayerInput.MovementDirection == Vector2.zero && Controller.Body.linearVelocity == Vector2.zero);
            AddTransition(PlayerStateType.Charge, () => PlayerInput.ChargeKeyPressed || PlayerInput.ChargeKeyHold);
        }
        
        private void Accelerate()
        {
            if (PlayerInput.MovementDirection.x != 0)
            {
                _linearVelocity.x = Mathf.Abs(Controller.Body.linearVelocity.x) > Controller.Stats.maxSpeed
                    ? Mathf.MoveTowards(_linearVelocity.x, 0.0f, Controller.Stats.decelerationSpeed * Time.deltaTime)
                    : _linearVelocity.x + PlayerInput.NormalizedMovementDirection.x * Controller.Stats.accelerationSpeed * Time.deltaTime;
            }
            
            if (PlayerInput.MovementDirection.y != 0)
            {
                _linearVelocity.y = Mathf.Abs(Controller.Body.linearVelocity.y) > Controller.Stats.maxSpeed
                    ? Mathf.MoveTowards(_linearVelocity.y, 0.0f, Controller.Stats.decelerationSpeed * Time.deltaTime)
                    : _linearVelocity.y + PlayerInput.NormalizedMovementDirection.y * Controller.Stats.accelerationSpeed * Time.deltaTime;
            }
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

        private void Brake()
        {
            if (PlayerInput.MovementDirection.x != 0 && PlayerInput.MovementDirection.x != Mathf.Sign(_linearVelocity.x))
            {
                _linearVelocity.x = Mathf.MoveTowards(_linearVelocity.x, 0.0f, Controller.Stats.brakeSpeed * Time.deltaTime);  
            }

            if (PlayerInput.MovementDirection.y != 0 && PlayerInput.MovementDirection.y != Mathf.Sign(_linearVelocity.y))
            {
                _linearVelocity.y = Mathf.MoveTowards(_linearVelocity.y, 0.0f, Controller.Stats.brakeSpeed * Time.deltaTime);
            }
        }
    }
}