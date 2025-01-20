using Entities.Player;
using FSM;
using UnityEngine;
using Utility;

namespace Entities.Enemy
{
    public class EnemyController : StateMachine<EnemyStateType>
    {
        [HideInInspector] public bool isHurt;
        
        private void Awake()
        {
            InitializeStateMachine(new EnemyStateFactory(this), EnemyStateType.Idle);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(UnityTag.PlayerDamageHitbox.ToString()))
            {
                isHurt = true;
                Debug.Log("Enemy got hit for: " + other.GetComponentInParent<PlayerController>().Stats.currentDamage + " damage.");
            }
        }
    }
}