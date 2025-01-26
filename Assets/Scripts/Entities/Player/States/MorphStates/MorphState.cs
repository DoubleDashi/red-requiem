using System.Collections;
using System.Collections.Generic;
using Entities.Player.States.PrimaryStates;
using UnityEngine;
using Utility;

namespace Entities.Player.States.MorphStates
{
    public abstract class MorphState : PlayerState
    {
        private readonly List<Collider2D> _interactedColliders = new();
        
        protected MorphState(PlayerController controller) : base(controller)
        {
        }
        
        protected void CollisionDetection()
        {
            Vector3 rotatedOffsetPosition = Quaternion.Euler(0, 0, Controller.transform.eulerAngles.z) * Controller.morph.config.collisionPointOffset;
            Vector3 positionWithOffset = Controller.morph.collisionPoint.position + rotatedOffsetPosition;

            Collider2D[] others = Physics2D.OverlapBoxAll(positionWithOffset, Controller.morph.config.collisionBox, Controller.transform.eulerAngles.z);
            foreach (Collider2D other in others)
            {
                if (other.CompareTag(UnityTag.Enemy.ToString()) && _interactedColliders.Contains(other) == false)
                {
                    other.GetComponentInParent<IEntity>().TakeDamage(new Damageable(
                        Controller.morph.config.damage, 
                        Controller.morph.config.enemyKnockbackForce,
                        (Controller.transform.position - other.transform.position).normalized,
                        Controller.morph.config.shakeIntensity
                    ));
                    _interactedColliders.Add(other);

                    if (Controller.morph.config.hasFireRate)
                    {
                        Controller.StartCoroutine(RemoveColliderAfterDelay(other, Controller.morph.config.fireRate));
                    }
                }
            }
        }

        protected void CollisionClear()
        {
            _interactedColliders.Clear();
        }
        
        private IEnumerator RemoveColliderAfterDelay(Collider2D collider, float delay)
        {
            yield return new WaitForSeconds(delay);
            _interactedColliders.Remove(collider);
        }
    }
}