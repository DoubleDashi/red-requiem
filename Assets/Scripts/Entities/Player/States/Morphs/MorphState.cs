using System.Collections.Generic;
using Entities.Enemy;
using UnityEngine;
using Utility;

namespace Entities.Player.States.Morphs
{
    public abstract class MorphState : PlayerState
    {
        private readonly float _detectionRadius;
        private readonly List<Collider2D> _interactedColliders = new();
        
        protected MorphState(PlayerController controller) : base(controller)
        {
            _detectionRadius = Controller.currentMorph.collisionRadius;
        }
        
        protected void CollisionDetection()
        {
            Collider2D[] others = Physics2D.OverlapCircleAll(Controller.transform.position, _detectionRadius);
            foreach (var other in others)
            {
                if (other.CompareTag(UnityTag.Enemy.ToString()) && _interactedColliders.Contains(other) == false)
                {
                    other.GetComponent<EnemyController>().TakeDamage(Controller.stats.currentDamage);
                    _interactedColliders.Add(other);
                }
            }
        }

        protected void CollisionClear()
        {
            _interactedColliders.Clear();
        }
    }
}