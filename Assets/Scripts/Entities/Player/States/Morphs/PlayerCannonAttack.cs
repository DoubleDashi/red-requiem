using Entities.Player.Components;
using UnityEngine;

namespace Entities.Player.States.Morphs
{
    public class PlayerCannonAttack : MorphState
    {
        public PlayerCannonAttack(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.cannonLine.enabled = true;
        }

        public override void Update()
        {
            Vector3 direction = Quaternion.Euler(0, 0, Controller.weaponPivot.eulerAngles.z) * Vector3.right;
            Controller.cannonLine.SetPosition(0, Controller.weaponPivot.position - direction * 0.15f);
            Controller.cannonLine.SetPosition(1, Controller.weaponPivot.position + direction * Controller.currentMorph.maxLength);
            
            CollisionDetection();
        }

        public override void FixedUpdate()
        {
            Controller.components.Movement.ForceDecelerate();
            
            Vector2 knockbackDirection = -Controller.weaponPivot.right;
            Controller.components.body.AddForce(knockbackDirection.normalized * Controller.currentMorph.selfKnockbackForce, ForceMode2D.Impulse);
        }

        public override void Exit()
        {
            Controller.cannonLine.enabled = false;
            Controller.currentMorph.collisionBox.x = 0f;
            Controller.currentMorph.collisionPointOffset.x = 0f;
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => Input.GetKey(KeyCode.Mouse0) == false);
        }
    }
}