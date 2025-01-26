using System.Collections.Generic;
using FSM;
using UnityEngine;
using Utility;

namespace Entities.Enemies.KitingEnemy.States
{
    public abstract class KitingEnemyState : BaseState<KitingEnemyStateType>
    {
        protected readonly KitingEnemyController Controller;
        
        protected Collider2D AggroTargetCollider;
        protected Collider2D AttackTargetCollider;
        protected Collider2D RunAwayTargetCollider;
        
        private readonly List<Collider2D> _interactedColliders = new();
        
        protected KitingEnemyState(KitingEnemyController controller)
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
        
        protected bool CollidersInRunAwayRange(UnityTag objectWithTag)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                Controller.transform.position, 
                Controller.stats.runAwayRadius
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
    }
}