using System.Collections;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerHurt : PlayerState
    {
        private bool _isComplete;
        
        private SpriteRenderer[] _spriteRenderer;
        private Color _originalColor;
        
        public PlayerHurt(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.components.Body.linearVelocity = Vector2.zero;
            
            _spriteRenderer = Controller.GetComponentsInChildren<SpriteRenderer>();
            _originalColor = _spriteRenderer[0].color;
            foreach (var spriteRenderer in _spriteRenderer)
            {
                spriteRenderer.color = Color.white;
            }
            
            Controller.StartCoroutine(HurtRoutine());
        }

        public override void Exit()
        {
            _isComplete = false;
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => _isComplete);
        }

        private IEnumerator HurtRoutine()
        {
            yield return new WaitForSeconds(0.1f);
            
            const float duration = 0.5f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                foreach (var spriteRenderer in _spriteRenderer)
                {
                    spriteRenderer.color = Color.Lerp(Color.white, _originalColor, elapsedTime / duration);
                }

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            foreach (var spriteRenderer in _spriteRenderer) 
            {
                spriteRenderer.color = _originalColor;
            }

            _isComplete = true;
        }
    }
}