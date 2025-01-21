using Entities.Player.Morphs;
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
            _linearVelocity = Controller.body.linearVelocity;
        }

        public override void Update()
        {
            Accelerate();
            Decelerate();
            Brake();
            
            Controller.body.linearVelocity = _linearVelocity;
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => PlayerInput.movementDirection == Vector2.zero && Controller.body.linearVelocity == Vector2.zero);
            AddTransition(PlayerStateType.Charge, () => (PlayerInput.chargeKeyPressed || PlayerInput.chargeKeyHold) && Controller.CurrentMorph.HasCharge);
            AddTransition(PlayerStateType.Attack, () => PlayerInput.chargeKeyPressed && Controller.CurrentMorph.HasCharge == false);
        }
        
        private void Accelerate()
        {
            if (PlayerInput.movementDirection.x != 0)
            {
                _linearVelocity.x = Mathf.Abs(Controller.body.linearVelocity.x) > Controller.stats.maxSpeed
                    ? Mathf.MoveTowards(_linearVelocity.x, 0.0f, Controller.stats.decelerationSpeed * Time.deltaTime)
                    : _linearVelocity.x + PlayerInput.normalizedMovementDirection.x * Controller.stats.accelerationSpeed * Time.deltaTime;
            }
            
            if (PlayerInput.movementDirection.y != 0)
            {
                _linearVelocity.y = Mathf.Abs(Controller.body.linearVelocity.y) > Controller.stats.maxSpeed
                    ? Mathf.MoveTowards(_linearVelocity.y, 0.0f, Controller.stats.decelerationSpeed * Time.deltaTime)
                    : _linearVelocity.y + PlayerInput.normalizedMovementDirection.y * Controller.stats.accelerationSpeed * Time.deltaTime;
            }
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

        private void Brake()
        {
            if (PlayerInput.movementDirection.x != 0 && PlayerInput.movementDirection.x != Mathf.Sign(_linearVelocity.x))
            {
                _linearVelocity.x = Mathf.MoveTowards(_linearVelocity.x, 0.0f, Controller.stats.brakeSpeed * Time.deltaTime);  
            }

            if (PlayerInput.movementDirection.y != 0 && PlayerInput.movementDirection.y != Mathf.Sign(_linearVelocity.y))
            {
                _linearVelocity.y = Mathf.MoveTowards(_linearVelocity.y, 0.0f, Controller.stats.brakeSpeed * Time.deltaTime);
            }
        }
    }
}