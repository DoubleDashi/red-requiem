using System;
using System.Collections;
using Configs;
using UnityEngine;

namespace Entities.Enemy.States
{
    public class EnemyHurt : EnemyState
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly Color _originalColor;
        
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
            _spriteRenderer.color = Color.white;
            Controller.StartCoroutine(HurtRoutine());
        }
        
        protected override void SetTransitions()
        {
            AddTransition(EnemyStateType.Death, () => Controller.Stats.health <= 0f);
            AddTransition(EnemyStateType.Idle, () => Controller.isHurt == false);
        }

        private IEnumerator HurtRoutine()
        {
            yield return new WaitForSeconds(0.1f);
            
            const float duration = 0.5f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                _spriteRenderer.color = Color.Lerp(Color.white, _originalColor, elapsedTime / duration);
                elapsedTime += Time.deltaTime;

                yield return null;
            }
            
            _spriteRenderer.color = _originalColor;
            Controller.isHurt = false;
        }

        private void HandleOnEnemyHurt(Guid guid, float damage)
        {
            if (guid != Controller.Stats.Guid)
            {
                return;
            }
            
            Controller.Stats.health -= damage;
        }
    }
}