using System;
using System.Collections;
using Configs.Events;
using Entities.Player.Factories;
using UnityEngine;

namespace Entities.Player.States.PrimaryStates
{
    public class PlayerHurt : PlayerState
    {
        
        private Coroutine _routine;
        
        public PlayerHurt(PlayerController controller) : base(controller)
        {
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
            AddTransition(PlayerStateType.Idle, () => Controller.isHurt == false);
        }

        private IEnumerator HurtRoutine()
        {
            Controller.spriteRenderer.material = Controller.whiteMaterial;
            
            yield return new WaitForSeconds(0.1f);

            Controller.spriteRenderer.material = Controller.originalMaterial;
            Controller.isHurt = false;
        }

        private void HandleOnHurt(Guid guid, Damageable damageable)
        {
            if (guid != Controller.stats.guid)
            {
                return;
            }

            Controller.EnableInCombat();
            
            CameraEventConfig.OnShake?.Invoke(damageable.ShakeIntensity);
            PlayerEventConfig.OnHurtSFX?.Invoke(guid);
            
            Controller.body.linearVelocity = Vector2.zero;
            Controller.body.linearDamping = 8f;
            
            Controller.body.AddForce(
                damageable.knockback,
                ForceMode2D.Impulse
            );

            
            float remainingArmor = Controller.stats.armor - damageable.ArmorPenetration;
            float penetration = remainingArmor == 0 && Controller.stats.armor == 0
                ? 1f
                : Mathf.Clamp01(1 - remainingArmor / Controller.stats.armor);
            Controller.stats.health -= damageable.Damage * penetration;
        }
    }
}