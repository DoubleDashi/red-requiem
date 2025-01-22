﻿using System.Collections;
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
        
        protected void CollisionDetection()
        {
            Vector3 rotatedOffsetPosition = Quaternion.Euler(0, 0, Controller.transform.eulerAngles.z) * Controller.currentMorph.collisionPointOffset;
            Vector3 positionWithOffset = Controller.weaponCollision.position + rotatedOffsetPosition;

            Collider2D[] others = Physics2D.OverlapBoxAll(positionWithOffset, Controller.currentMorph.collisionBox, Controller.transform.eulerAngles.z);
            
            foreach (Collider2D other in others)
            {
                if (other.CompareTag(UnityTag.Enemy.ToString()) && _interactedColliders.Contains(other) == false)
                {
                    other.GetComponent<EnemyController>().TakeDamage(Controller.stats.currentDamage);
                    _interactedColliders.Add(other);

                    if (Controller.currentMorph.hasFireRate)
                    {
                        Debug.Log("Removed");
                        Controller.StartCoroutine(RemoveColliderAfterDelay(other, Controller.currentMorph.fireRate));
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