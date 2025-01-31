using System;
using System.Collections;
using Configs.Events;
using UnityEngine;

namespace Entities.Enemies.KitingEnemy.States
{
    public class KitingEnemyHurt : KitingEnemyState
    {
        private Coroutine _routine;
        
        
        public KitingEnemyHurt(KitingEnemyController controller) : base(controller)
        {
        }

        public override void Subscribe()
        {
            KitingEnemyEventConfig.OnHurt += HandleOnEnemyHurt;
        }
        
        public override void Unsubscribe()
        {
            KitingEnemyEventConfig.OnHurt -= HandleOnEnemyHurt;
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
            AddTransition(KitingEnemyStateType.Death, () => Controller.stats.health <= 0f);
            AddTransition(KitingEnemyStateType.Idle, () => Controller.isHurt == false);
        }

        private IEnumerator HurtRoutine()
        {
            Controller.spriteRenderer.material = Controller.whiteMaterial;
            
            yield return new WaitForSeconds(0.1f);

            Controller.spriteRenderer.material = Controller.originalMaterial;
            Controller.isHurt = false;
        }

        private void HandleOnEnemyHurt(Guid guid, Damageable damageable)
        {
            if (guid != Controller.stats.guid)
            {
                return;
            }

            CameraEventConfig.OnShake?.Invoke(damageable.ShakeIntensity);
            KitingEnemyEventConfig.OnHurtSFX?.Invoke(guid);
            
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
            
            FloatingCombatTextEventConfig.OnHurt?.Invoke(Controller, damageable.Damage * penetration);
            Controller.stats.health -= damageable.Damage * penetration;
        }
    }
}