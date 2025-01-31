using UnityEngine;
using Utility;

namespace Entities.Player
{
    public class PlayerMovement
    {
        private Vector2 _linearVelocity;
        private Vector2 _previousPosition;
        
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
            _linearVelocity.x = Mathf.Lerp(_linearVelocity.x, 0f, _controller.stats.decelerationSpeed * Time.deltaTime);
            _linearVelocity.y = Mathf.Lerp(_linearVelocity.y, 0f, _controller.stats.decelerationSpeed * Time.deltaTime);
            
            _controller.body.linearVelocity = _linearVelocity;
            
            if (_linearVelocity.magnitude < 0.1f)
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
            _linearVelocity = new Vector2(
                Mathf.Clamp(_linearVelocity.x, -_controller.stats.maxSpeed, _controller.stats.maxSpeed),
                Mathf.Clamp(_linearVelocity.y, -_controller.stats.maxSpeed, _controller.stats.maxSpeed)
            );
            
            _controller.body.linearVelocity = _linearVelocity;
        }
        
        private bool IsColliding()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_controller.transform.position, 0.1f);
            foreach (var collider in colliders)
            {
                if (collider.gameObject != _controller.gameObject)
                {
                    return true;
                }
            }
            return false;
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
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

            // Rotate the parent object
            _controller.transform.rotation = Quaternion.RotateTowards(_controller.transform.rotation, targetRotation, _controller.stats.rotationSpeed * Time.deltaTime);

            // Counter-rotate the child object with the SpriteRenderer
            if (_controller.spriteRenderer)
            {
                _controller.spriteRenderer.transform.rotation = Quaternion.identity;
            }
            
            // if (_controller.stats.disableRotation)
            // {
            //     return;
            // }
            //
            // Vector2 mousePosition = _controller.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            // Vector2 direction = (mousePosition - (Vector2)_controller.transform.position).normalized;
            // Vector2 position = direction.normalized * 0.25f;
            //
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Quaternion target = Quaternion.Euler(0f, 0f, angle);
            //
            // _controller.morph.pivotPoint.rotation = Quaternion.RotateTowards(_controller.morph.pivotPoint.rotation, target, _controller.stats.rotationSpeed * Time.deltaTime);
            // _controller.morph.pivotPoint.position = _controller.transform.position + (Vector3)position;
        }
    }
}