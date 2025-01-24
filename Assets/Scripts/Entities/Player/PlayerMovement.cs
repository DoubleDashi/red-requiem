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
                _linearVelocity.x = Mathf.Abs(_controller.body.linearVelocity.x) > _controller.stats.maxSpeed
                    ? Mathf.MoveTowards(_linearVelocity.x, 0.0f, _controller.stats.decelerationSpeed * Time.deltaTime)
                    : _linearVelocity.x + PlayerInput.normalizedMovementDirection.x * _controller.stats.accelerationSpeed * Time.deltaTime;
            }
            
            if (PlayerInput.movementDirection.y != 0)
            {
                _linearVelocity.y = Mathf.Abs(_controller.body.linearVelocity.y) > _controller.stats.maxSpeed
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
            Vector2 velocity = _controller.body.linearVelocity;
            
            velocity.x = Mathf.Lerp(velocity.x, 0f, _controller.stats.decelerationSpeed * Time.deltaTime);
            velocity.y = Mathf.Lerp(velocity.y, 0f, _controller.stats.decelerationSpeed * Time.deltaTime);
            
            _controller.body.linearVelocity = velocity;
            
            if (velocity.magnitude < 0.1f)
            {
                _controller.body.linearVelocity = Vector2.zero;
            }
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
            _controller.body.linearVelocity = _linearVelocity;
        }
        
        public void Rotate()
        {
            if (_controller.stats.disableRotation)
            {
                return;
            }
            
            Vector2 mousePosition = _controller.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)_controller.transform.position).normalized;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(0f, 0f, angle);
            
            _controller.transform.rotation = Quaternion.RotateTowards(_controller.transform.rotation, target, _controller.stats.rotationSpeed * Time.deltaTime);
        }
    }
}