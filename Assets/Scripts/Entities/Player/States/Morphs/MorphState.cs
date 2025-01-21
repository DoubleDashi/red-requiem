using System.Collections.Generic;
using Entities.Enemy;
using UnityEngine;
using Utility;

namespace Entities.Player.States.Morphs
{
    public abstract class MorphState : PlayerState
    {
        private readonly float _detectionRadius;
        protected readonly List<Collider2D> InteractedColliders = new();
        
        protected MorphState(PlayerController controller) : base(controller)
        {
            _detectionRadius = Controller.currentMorph.collisionRadius;
        }
        
        protected void CollisionDetection()
        {
            Collider2D[] others = Physics2D.OverlapCircleAll(Controller.transform.position, _detectionRadius);
            foreach (var other in others)
            {
                if (other.CompareTag(UnityTag.Enemy.ToString()) && InteractedColliders.Contains(other) == false)
                {
                    other.GetComponent<EnemyController>().TakeDamage(Controller.stats.currentDamage);
                    InteractedColliders.Add(other);
                }
            }
        }

        protected void CollisionClear()
        {
            InteractedColliders.Clear();
        }
    }
}