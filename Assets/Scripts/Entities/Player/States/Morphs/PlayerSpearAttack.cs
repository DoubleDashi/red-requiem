﻿using Configs;
using Entities.Player.Components;
using UnityEngine;

namespace Entities.Player.States.Morphs
{
    public class PlayerSpearAttack : MorphState
    {
        private float _decelerationSpeed;
        
        public PlayerSpearAttack(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.components.body.linearVelocity = Vector2.zero;
            PlayerEventConfig.OnPlayerMove?.Invoke(Controller.stats.guid);

            _decelerationSpeed = Controller.stats.decelerationSpeed;
            Controller.stats.decelerationSpeed = 15f;
            
            Vector2 mousePosition = Controller.components.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2) Controller.transform.position).normalized;
            
            Controller.components.body.linearVelocity = direction * Controller.stats.currentChargeSpeed;
        }

        public override void Update()
        {
            CollisionDetection();
            Decelerate();
            SetDamage();
        }

        public override void Exit()
        {
            CollisionClear();
            Controller.stats.decelerationSpeed = _decelerationSpeed;
        }
        
        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => Controller.components.body.linearVelocity == Vector2.zero);
        }
        
        private void Decelerate()
        {
            Vector2 velocity = Controller.components.body.linearVelocity;
            
            velocity.x = Mathf.Lerp(velocity.x, 0f, Controller.stats.decelerationSpeed * Time.deltaTime);
            velocity.y = Mathf.Lerp(velocity.y, 0f, Controller.stats.decelerationSpeed * Time.deltaTime);
            
            Controller.components.body.linearVelocity = velocity;
            
            if (velocity.magnitude < 0.1f)
            {
                Controller.components.body.linearVelocity = Vector2.zero;
            }
        }

        private void SetDamage()
        {
            Controller.stats.currentDamage = Mathf.Lerp(Controller.stats.minDamage, Controller.stats.maxDamage, Controller.components.body.linearVelocity.magnitude / Controller.stats.maxChargeSpeed);
        }
    }
}