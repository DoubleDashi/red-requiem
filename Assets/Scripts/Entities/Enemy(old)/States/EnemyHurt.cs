using System;
using System.Collections;
using Configs.Events;
using UnityEngine;

namespace Entities.Enemy.States
{
    public class EnemyHurt : EnemyState
    {
        private const float Duration = 0.5f;
        private readonly SpriteRenderer _spriteRenderer;
        private readonly Color _originalColor;

        private float _elapsedTime;
        private Coroutine _hurtRoutine;
        
        public EnemyHurt(EnemyController controller) : base(controller)
        {
            _spriteRenderer = Controller.GetComponent<SpriteRenderer>();
            _originalColor = _spriteRenderer.color;
        }

        public override void Subscribe()
        {
            EnemyEventConfig.OnEnemyHurt += HandleOnEnemyHurt;
        }
        
        public override void Unsubscribe()
        {
            EnemyEventConfig.OnEnemyHurt -= HandleOnEnemyHurt;
        }

        public override void Enter()
        {
            Controller.body.bodyType = RigidbodyType2D.Dynamic;
            Controller.isHurt = false;
            _elapsedTime = 0f;
            
            if (_hurtRoutine != null)
            {
                Controller.StopCoroutine(_hurtRoutine);
                _hurtRoutine = null;
            }
            
            _spriteRenderer.color = Color.white;
            _hurtRoutine = Controller.StartCoroutine(HurtRoutine());
        }

        public override void Exit()
        {
            Controller.body.bodyType = RigidbodyType2D.Kinematic;
        }
        
        protected override void SetTransitions()
        {
            AddTransition(EnemyStateType.Death, () => Controller.stats.health <= 0f);
            AddTransition(EnemyStateType.Idle, () => Controller.isHurt == false && _elapsedTime >= Duration);
        }

        private IEnumerator HurtRoutine()
        {
            yield return new WaitForSeconds(0.1f);
            
            while (_elapsedTime < Duration)
            {
                _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _originalColor, _elapsedTime / Duration);
                _elapsedTime += Time.deltaTime;

                yield return null;
            }
            
            _spriteRenderer.color = _originalColor;
            Controller.isHurt = false;
        }

        private void HandleOnEnemyHurt(Guid guid, Damageable damageable)
        {
            if (guid != Controller.stats.guid)
            {
                return;
            }

            StationaryEnemyEventConfig.OnHurtSFX?.Invoke(guid);
            
            Controller.body.bodyType = RigidbodyType2D.Dynamic;
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