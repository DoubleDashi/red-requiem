using UnityEngine;

namespace Entities.Player
{
    public class PlayerMovement
    {
        private Vector2 _linearVelocity;
        
        private readonly PlayerController _controller;
        
        public PlayerMovement(PlayerController controller)
        {
            _controller = controller;
        }
        
        public void Accelerate()
        {
            if (PlayerInput.movementDirection.x != 0)
            {
                _linearVelocity.x = Mathf.Abs(_controller.components.Body.linearVelocity.x) > _controller.stats.maxSpeed
                    ? Mathf.MoveTowards(_linearVelocity.x, 0.0f, _controller.stats.decelerationSpeed * Time.deltaTime)
                    : _linearVelocity.x + PlayerInput.normalizedMovementDirection.x * _controller.stats.accelerationSpeed * Time.deltaTime;
            }
            
            if (PlayerInput.movementDirection.y != 0)
            {
                _linearVelocity.y = Mathf.Abs(_controller.components.Body.linearVelocity.y) > _controller.stats.maxSpeed
                    ? Mathf.MoveTowards(_linearVelocity.y, 0.0f, _controller.stats.decelerationSpeed * Time.deltaTime)
                    : _linearVelocity.y + PlayerInput.normalizedMovementDirection.y * _controller.stats.accelerationSpeed * Time.deltaTime;
            }
        }

        public void Decelerate()
        {
            if (PlayerInput.movementDirection.x == 0)
            {
                _linearVelocity.x = Mathf.MoveTowards(_linearVelocity.x, 0.0f, _controller.stats.decelerationSpeed * Time.deltaTime);  
            }
            
            if (PlayerInput.movementDirection.y == 0)
            {
                _linearVelocity.y = Mathf.MoveTowards(_linearVelocity.y, 0.0f, _controller.stats.decelerationSpeed * Time.deltaTime); 
            }
        }

        public void ForceDecelerate()
        {
            _linearVelocity.x = Mathf.MoveTowards(_linearVelocity.x, 0.0f, _controller.stats.decelerationSpeed * Time.deltaTime);
            _linearVelocity.y = Mathf.MoveTowards(_linearVelocity.y, 0.0f, _controller.stats.decelerationSpeed * Time.deltaTime);
        }

        public void Brake()
        {
            if (PlayerInput.movementDirection.x != 0 && PlayerInput.movementDirection.x != Mathf.Sign(_linearVelocity.x))
            {
                _linearVelocity.x = Mathf.MoveTowards(_linearVelocity.x, 0.0f, _controller.stats.brakeSpeed * Time.deltaTime);  
            }

            if (PlayerInput.movementDirection.y != 0 && PlayerInput.movementDirection.y != Mathf.Sign(_linearVelocity.y))
            {
                _linearVelocity.y = Mathf.MoveTowards(_linearVelocity.y, 0.0f, _controller.stats.brakeSpeed * Time.deltaTime);
            }
        }
        
        public void SetLinearVelocity()
        {
            _controller.components.Body.linearVelocity = _linearVelocity;
        }
    }
}