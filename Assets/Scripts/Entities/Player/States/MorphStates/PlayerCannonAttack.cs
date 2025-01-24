using Entities.Player.Factories;
using UnityEngine;

namespace Entities.Player.States.MorphStates
{
    public class PlayerCannonAttack : MorphState
    {
        public PlayerCannonAttack(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.morph.lineRenderer.enabled = true;
        }

        public override void Update()
        {
            Vector3 direction = Quaternion.Euler(0, 0, Controller.morph.pivotPoint.eulerAngles.z) * Vector3.right;
            Controller.morph.lineRenderer.SetPosition(0, Controller.morph.pivotPoint.position - direction * 0.15f);
            Controller.morph.lineRenderer.SetPosition(1, Controller.morph.pivotPoint.position + direction * Controller.morph.config.maxLength);
            
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
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => Input.GetKey(KeyCode.Mouse0) == false);
        }
    }
}