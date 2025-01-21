﻿using System.Collections;
using Entities.Player.Components;
using UnityEngine;

namespace Entities.Player.States.Morphs
{
    public class PlayerSwordAttack : MorphState
    {
        private bool _isComplete;
        
        public PlayerSwordAttack(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.StartCoroutine(AttackRoutine());
        }

        public override void Update()
        {
            CollisionDetection();
            Controller.components.Movement.ForceDecelerate();
        }

        public override void Exit()
        {
            CollisionClear();
            _isComplete = false;
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => _isComplete && Controller.components.body.linearVelocity == Vector2.zero);
            AddTransition(PlayerStateType.Move, () => _isComplete && Controller.components.body.linearVelocity != Vector2.zero);
        }

        private IEnumerator AttackRoutine()
        {
            yield return new WaitForSeconds(0.15f);

            _isComplete = true;
        }
    }
}