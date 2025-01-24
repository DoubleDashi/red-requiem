﻿using Configs;
using Entities.Player.Factories;
using Entities.Player.States.MorphStates;
using UnityEngine;

namespace Entities.Player.States.Morphs
{
    public class PlayerSpearCharge : MorphState
    {
        private bool _usedSFX;
        private Vector2 _linearVelocity;
        
        public PlayerSpearCharge(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.stats.currentChargeSpeed = 0f;
        }
        
        public override void Update()
        {
            Accelerate();
            Rotate();

            if (IsCharged() && _usedSFX == false)
            {
                PlayerEventConfig.OnPlayerChargeComplete?.Invoke(Controller.stats.guid);
                _usedSFX = true;
            }
        }
        
        public override void FixedUpdate()
        {
            Controller.Movement.ForceDecelerate();
        }

        public override void Exit()
        {
            _usedSFX = false;
        }
        
        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => PlayerInput.chargeCancelKeyPressed);
            AddTransition(PlayerStateType.SpearAttack, () => PlayerInput.chargeKeyReleased);
        }
        
        private void Accelerate()
        {
            Controller.stats.currentChargeSpeed += Controller.stats.chargeSpeed * Time.deltaTime;
            Controller.stats.currentChargeSpeed = Mathf.Clamp(Controller.stats.currentChargeSpeed, 0f, Controller.stats.maxChargeSpeed);
        }
        
        private bool IsCharged()
        {
            return Controller.stats.currentChargeSpeed == Controller.stats.maxChargeSpeed;
        }

        private void Rotate()
        {
            Vector2 mousePosition = Controller.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2) Controller.transform.position).normalized;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(0f, 0f, angle);
            
            Controller.transform.rotation = Quaternion.RotateTowards(Controller.transform.rotation, target, Controller.stats.rotationSpeed * Time.deltaTime);
        }
    }
}