using System;
using Entities.Player.Components;
using FSM;
using UnityEngine;

namespace Entities.Player.Controllers
{
    [Serializable]
    public class PlayerComponents
    {
        public PlayerMovement Movement;
        public Rigidbody2D body;
        public Camera mainCamera;
    }
    
    public class PlayerController : StateMachine<PlayerStateType>
    {
        [SerializeField] private EntityStats entityStats;
        [SerializeField] private PlayerComponents playerComponents;
        [SerializeField] private PlayerAttackController attackController;
        
        public PlayerComponents components => playerComponents;
        public PlayerAttackController attack => attackController;
        public EntityStats stats => entityStats;

        private void Awake()
        {
            components.mainCamera = Camera.main;
            components.body = GetComponent<Rigidbody2D>();
            components.Movement = new PlayerMovement(this);
            
            InitializeStateMachine(new PlayerStateFactory(this), PlayerStateType.Idle);
        }

        protected override void Update()
        {
            base.Update();
            Rotate();
        }

        protected override void SetGlobalTransitions()
        {
            // AddGlobalTransition(PlayerStateType.Hurt, () => Input.GetKeyDown(KeyCode.Mouse1));
        }
        
        private void Rotate()
        {
            if (stats.disableRotation)
            {
                return;
            }
            
            Vector2 mousePosition = components.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(0f, 0f, angle);
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, stats.rotationSpeed * Time.deltaTime);
        }
    }
}
