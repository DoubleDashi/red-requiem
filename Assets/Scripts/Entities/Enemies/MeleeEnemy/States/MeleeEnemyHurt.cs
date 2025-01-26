using System;
using System.Collections;
using Configs.Events;
using UnityEngine;

namespace Entities.Enemies.MeleeEnemy.States
{
    public class MeleeEnemyHurt : MeleeEnemyState
    {
        private const float Duration = 0.5f;
        
        private Coroutine _routine;
        private float _elapsedTime;
        
        private readonly Color _originalColor;
        
        public MeleeEnemyHurt(MeleeEnemyController controller) : base(controller)
        {
            _originalColor = Controller.spriteRenderer.color;
        }

        public override void Subscribe()
        {
            MeleeEnemyEventConfig.OnHurt += HandleOnEnemyHurt;
        }
        
        public override void Unsubscribe()
        {
            MeleeEnemyEventConfig.OnHurt -= HandleOnEnemyHurt;
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
            AddTransition(MeleeEnemyStateType.Death, () => Controller.stats.health <= 0f);
            AddTransition(MeleeEnemyStateType.Idle, () => AnimationComplete() && Controller.isHurt == false);
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

        private void HandleOnEnemyHurt(Guid guid, Damageable damageable)
        {
            if (guid != Controller.stats.guid)
            {
                return;
            }

            CameraEventConfig.OnShake?.Invoke(damageable.ShakeIntensity);
            MeleeEnemyEventConfig.OnHurtSFX?.Invoke(guid);
            
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