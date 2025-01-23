using System.Collections;
using System.Collections.Generic;
using Entities.Enemy;
using UnityEngine;
using Utility;

namespace Entities.Player.States.Morphs
{
    public abstract class MorphState : PlayerState
    {
        private readonly List<Collider2D> _interactedColliders = new();
        
        protected MorphState(PlayerController controller) : base(controller)
        {
        }
        
        protected bool CollisionDetection()
        {
            bool collided = false;   
            Vector3 rotatedOffsetPosition = Quaternion.Euler(0, 0, Controller.transform.eulerAngles.z) * Controller.currentMorph.collisionPointOffset;
            Vector3 positionWithOffset = Controller.weaponCollision.position + rotatedOffsetPosition;

            Collider2D[] others = Physics2D.OverlapBoxAll(positionWithOffset, Controller.currentMorph.collisionBox, Controller.transform.eulerAngles.z);
            
            foreach (Collider2D other in others)
            {
                if (other.CompareTag(UnityTag.Enemy.ToString()) && _interactedColliders.Contains(other) == false)
                {
                    other.GetComponent<EnemyController>().TakeDamage(
                        Controller.currentMorph.damage, 
                        Controller.currentMorph.enemyKnockbackForce,
                        (Controller.transform.position - other.transform.position).normalized
                    );
                    _interactedColliders.Add(other);
                    collided = true;

                    if (Controller.currentMorph.hasFireRate)
                    {
                        Controller.StartCoroutine(RemoveColliderAfterDelay(other, Controller.currentMorph.fireRate));
                    }
                }
            }

            return collided;
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