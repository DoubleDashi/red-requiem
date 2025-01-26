using System;
using System.Collections;
using Configs.Events;
using Entities.Player.Factories;
using UnityEngine;

namespace Entities.Player.States.PrimaryStates
{
    public class PlayerHurt : PlayerState
    {
        private const float Duration = 0.5f;
        
        private Coroutine _routine;
        private float _elapsedTime;
        
        private readonly Color _originalColor;
        
        public PlayerHurt(PlayerController controller) : base(controller)
        {
            _originalColor = Controller.spriteRenderer.color;
        }

        public override void Subscribe()
        {
            PlayerEventConfig.OnHurt += HandleOnHurt;
        }
        
        public override void Unsubscribe()
        {
            PlayerEventConfig.OnHurt -= HandleOnHurt;
        }

        public override void Enter()
        {
            Controller.isHurt = false;
            
            _elapsedTime = 0f;

            if (_routine != null)
            {
                Controller.StopCoroutine(_routine);
                _routine = null;
            }

            Controller.spriteRenderer.color = Color.white;
            _routine = Controller.StartCoroutine(HurtRoutine());
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Death, () => Controller.stats.health <= 0f);
            AddTransition(PlayerStateType.Idle, () => AnimationComplete() && Controller.isHurt == false);
        }

        private IEnumerator HurtRoutine()
        {
            yield return new WaitForSeconds(0.1f);

            while (_elapsedTime < Duration)
            {
                Controller.spriteRenderer.color = Color.Lerp(
                    a: Controller.spriteRenderer.color,
                    b: _originalColor,
                    t: _elapsedTime / Duration
                );

                _elapsedTime += Time.deltaTime;
                yield return null;
            }

            Controller.spriteRenderer.color = _originalColor;
            Controller.isHurt = false;
        }

        private bool AnimationComplete()
        {
            return _elapsedTime >= Duration;
        }

        private void HandleOnHurt(Guid guid, Damageable damageable)
        {
            if (guid != Controller.stats.guid)
            {
                return;
            }

            Debug.Log($"Damage: {damageable.Damage}, Knockback: {damageable.knockback}, Shake: {damageable.ShakeIntensity}");
            
            CameraEventConfig.OnShake?.Invoke(damageable.ShakeIntensity);
            PlayerEventConfig.OnHurtSFX?.Invoke(guid);
            
            Controller.body.linearVelocity = Vector2.zero;
            Controller.body.linearDamping = 8f;
            
            Controller.body.AddForce(
                damageable.knockback,
                ForceMode2D.Impulse
            );
            
            Controller.stats.health -= damageable.Damage;
        }
    }
}