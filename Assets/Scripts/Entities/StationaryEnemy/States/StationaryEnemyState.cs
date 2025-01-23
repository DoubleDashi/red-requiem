using FSM;
using UnityEngine;
using Utility;

namespace Entities.StationaryEnemy.States
{
    public abstract class StationaryEnemyState : BaseState<StationaryEnemyStateType>
    {
        protected readonly StationaryEnemyController Controller;
        
        protected StationaryEnemyState(StationaryEnemyController controller)
        {
            Controller = controller;
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