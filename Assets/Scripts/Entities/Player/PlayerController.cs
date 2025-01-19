using FSM;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerController : StateMachine<PlayerStateType>
    {
        [SerializeField] private EntityStats entityStats;

        public EntityStats stats => entityStats;
        public Rigidbody2D body { get; private set; }
        
        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            
            InitializeStateMachine(new PlayerStateFactory(this), PlayerStateType.Idle);
        }
    }
}
