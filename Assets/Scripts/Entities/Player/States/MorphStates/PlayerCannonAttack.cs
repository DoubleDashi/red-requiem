using System.Collections;
using Configs.Events;
using Entities.Player.Factories;
using UnityEngine;

namespace Entities.Player.States.MorphStates
{
    public class PlayerCannonAttack : MorphState
    {
        private bool _playShoot = true;
        
        public PlayerCannonAttack(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.morph.lineRenderer.enabled = true;
            
            CameraEventConfig.OnConsistentShakeStart?.Invoke(0.35f);
        }

        public override void Update()
        {
            if (_playShoot)
            {
                PlayerEventConfig.OnCannonShootSFX?.Invoke(Controller.stats.guid, 0.35f);
                _playShoot = false;
                Controller.StartCoroutine(EnableShootSFX());
            }
            
            Vector3 direction = Quaternion.Euler(0, 0, Controller.morph.pivotPoint.eulerAngles.z) * Vector3.right;
            Controller.morph.lineRenderer.SetPosition(0, Controller.morph.pivotPoint.position - direction * 0.15f);
            Controller.morph.lineRenderer.SetPosition(1, Controller.morph.pivotPoint.position + direction * Controller.morph.config.maxLength);
            Controller.morph.config.collisionPointOffset = new Vector2(Controller.morph.config.maxLength / 2f, Controller.morph.config.collisionPointOffset.y);
            Controller.morph.config.collisionBox = new Vector2(Controller.morph.config.maxLength, Controller.morph.config.collisionBox.y);
            
            LineRendererCollisionDetection(direction);
            CollisionDetection();
        }

        public override void FixedUpdate()
        {
            Controller.Movement.ForceDecelerate();
            
            Vector2 knockbackDirection = -Controller.morph.pivotPoint.right;
            Controller.body.AddForce(knockbackDirection.normalized * Controller.morph.config.selfKnockbackForce, ForceMode2D.Impulse);
        }

        public override void Exit()
        {
            Controller.morph.lineRenderer.enabled = false;
            Controller.morph.config.collisionBox.x = 0f;
            Controller.morph.config.collisionPointOffset.x = 0f;
            
            CameraEventConfig.OnConsistentShakeStop?.Invoke();
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => Input.GetKey(KeyCode.Mouse0) == false);
        }
        
        private void LineRendererCollisionDetection(Vector3 direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                Controller.morph.pivotPoint.position, 
                direction.normalized,
                Controller.morph.config.maxLength
            );

            if (hit)
            {
                Controller.morph.lineRenderer.SetPosition(1, hit.point);
                Controller.morph.config.collisionPointOffset = new Vector2(hit.distance / 2f, Controller.morph.config.collisionPointOffset.y);
                Controller.morph.config.collisionBox = new Vector2(hit.distance, Controller.morph.config.collisionBox.y);
            }
        }
        
        private IEnumerator EnableShootSFX()
        {
            yield return new WaitForSeconds(Controller.morph.config.fireRate);
            _playShoot = true;
        }
    }
}