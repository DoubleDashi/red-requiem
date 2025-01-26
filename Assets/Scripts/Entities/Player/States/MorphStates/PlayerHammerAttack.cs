using System.Collections;
using Configs.Events;
using Entities.Player.Factories;
using UnityEngine;

namespace Entities.Player.States.MorphStates
{
    public class PlayerHammerAttack : MorphState
    {
        private bool _isComplete;
        
        public PlayerHammerAttack(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            CameraEventConfig.OnShake?.Invoke(Controller.morph.config.shakeIntensity);
            PlayerEventConfig.OnHammerDownSFX?.Invoke(Controller.stats.guid);
            
            Controller.StartCoroutine(AttackRoutine());
        }

        public override void Update()
        {
            CollisionDetection();
        }
        
        public override void FixedUpdate()
        {
            Controller.Movement.ForceDecelerate();
        }

        public override void Exit()
        {
            CollisionClear();
            _isComplete = false;
        }
        
        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => _isComplete && Controller.body.linearVelocity == Vector2.zero);
            AddTransition(PlayerStateType.Move, () => _isComplete && Controller.body.linearVelocity != Vector2.zero);
        }
        
        private IEnumerator AttackRoutine()
        {
            yield return new WaitForSeconds(0.15f);

            _isComplete = true;
        }
        
    }
}