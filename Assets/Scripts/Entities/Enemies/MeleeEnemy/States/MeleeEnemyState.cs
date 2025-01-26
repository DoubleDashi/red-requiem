using System.Collections.Generic;
using FSM;
using UnityEngine;
using Utility;

namespace Entities.Enemies.MeleeEnemy.States
{
    public abstract class MeleeEnemyState: BaseState<MeleeEnemyStateType>
    {
        protected readonly MeleeEnemyController Controller;
        
        protected Collider2D AggroTargetCollider;
        protected Collider2D AttackTargetCollider;
        
        private readonly List<Collider2D> _interactedColliders = new();
        
        protected MeleeEnemyState(MeleeEnemyController controller)
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
        
        protected void CollisionDetection(UnityTag objectWithTag)
        {
            Vector3 rotatedOffsetPosition = Quaternion.Euler(0, 0, Controller.transform.eulerAngles.z) * Controller.weapon.collisionPointOffset;
            Vector3 positionWithOffset = Controller.weapon.collisionPoint.position + rotatedOffsetPosition;

            Collider2D[] others = Physics2D.OverlapBoxAll(positionWithOffset, Controller.weapon.collisionBox, Controller.transform.eulerAngles.z);
            foreach (Collider2D other in others)
            {
                if (other.CompareTag(objectWithTag.ToString()) && _interactedColliders.Contains(other) == false)
                {
                    other.GetComponentInParent<IEntity>().TakeDamage(new Damageable(
                        Controller.weapon.damage, 
                        Controller.weapon.enemyKnockbackForce,
                        (Controller.transform.position - other.transform.position).normalized,
                        Controller.weapon.shakeIntensity
                    ));
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