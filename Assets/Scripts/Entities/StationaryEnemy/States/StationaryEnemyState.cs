using System.Collections.Generic;
using FSM;
using UnityEngine;
using Utility;

namespace Entities.StationaryEnemy.States
{
    public abstract class StationaryEnemyState : BaseState<StationaryEnemyStateType>
    {
        protected readonly StationaryEnemyController Controller;
        
        protected Collider2D AggroTargetCollider;
        protected Collider2D AttackTargetCollider;
        
        private readonly List<Collider2D> _interactedColliders = new();
        
        protected StationaryEnemyState(StationaryEnemyController controller)
        {
            Controller = controller;
        }
        
        protected bool CollidersInAggroRange(UnityTag objectWithTag)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                Controller.transform.position, 
                Controller.stats.detectionRadius
                );
            
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag(objectWithTag.ToString()))
                {
                    AggroTargetCollider = collider;
                    return true;
                }
            }

            AggroTargetCollider = null;
            return false;
        }
        
        protected bool CollidersInAttackRange(UnityTag objectWithTag)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                Controller.transform.position, 
                Controller.stats.attackRadius
                );
            
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag(objectWithTag.ToString()))
                {
                    AttackTargetCollider = collider;
                    return true;
                }
            }

            AttackTargetCollider = null;
            return false;
        }

        protected bool DetectCollider(UnityTag objectWithTag)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                Controller.transform.position, 
                Controller.stats.detectionRadius
            );
            
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag(objectWithTag.ToString()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}