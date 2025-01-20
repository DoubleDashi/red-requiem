using FSM;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerController : StateMachine<PlayerStateType>
    {
        [SerializeField] private EntityStats entityStats;

        public EntityStats Stats => entityStats;
        public Rigidbody2D Body { get; private set; }
        public Camera MainCamera { get; private set; }
        
        private void Awake()
        {
            MainCamera = Camera.main;
            Body = GetComponent<Rigidbody2D>();
            
            InitializeStateMachine(new PlayerStateFactory(this), PlayerStateType.Idle);
        }
    }
}
