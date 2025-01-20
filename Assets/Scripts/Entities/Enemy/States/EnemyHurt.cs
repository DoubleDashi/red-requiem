using System.Collections;
using Configs;
using UnityEngine;

namespace Entities.Enemy.States
{
    public class EnemyHurt : EnemyState
    {
        private SpriteRenderer _spriteRenderer;
        private Color _originalColor;
        
        public EnemyHurt(EnemyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            EnemyEventConfig.OnEnemyHurt?.Invoke();
            
            _spriteRenderer = Controller.GetComponent<SpriteRenderer>();
            _originalColor = _spriteRenderer.color;
            
            _spriteRenderer.color = Color.white;
            
            Controller.StartCoroutine(HurtRoutine());
        }
        
        protected override void SetTransitions()
        {
            AddTransition(EnemyStateType.Idle, () => Controller.isHurt == false);
        }

        private IEnumerator HurtRoutine()
        {
            
            yield return new WaitForSeconds(0.1f);

            float duration = 0.5f;
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
    }
}