using System;
using System.Collections.Generic;
using Configs;
using Entities.Player.Components;
using FSM;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Player
{
    [Serializable]
    public class PlayerComponents
    {
        public PlayerMorphFactory MorphFactory;
        public PlayerMovement Movement;
        public Rigidbody2D body;
        public Camera mainCamera;
    }
    
    public class PlayerController : StateMachine<PlayerStateType>
    {
        [SerializeField] private EntityStats entityStats;
        [SerializeField] private List<MorphConfig> morphConfigs;

        [HideInInspector] public MorphConfig currentMorph;

        public PlayerComponents components { get; } = new();

        public EntityStats stats => entityStats;

        private void Awake()
        {
            components.mainCamera = Camera.main;
            components.body = GetComponent<Rigidbody2D>();
            components.Movement = new PlayerMovement(this);
            components.MorphFactory = new PlayerMorphFactory(morphConfigs);

            currentMorph = components.MorphFactory.FindByType(MorphType.Spear);
            
            InitializeStateMachine(new PlayerStateFactory(this), PlayerStateType.Idle);
        }

        protected override void Update()
        {
            base.Update();
            components.Movement.Rotate();
        }

        protected override void SetGlobalTransitions()
        {
            // AddGlobalTransition(PlayerStateType.Hurt, () => Input.GetKeyDown(KeyCode.Mouse1));
        }
        
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            Debug.Log("Current morph: " + currentMorph + ", collision radius: " + currentMorph?.collisionRadius);
            
            if (currentMorph == null || currentMorph.collisionRadius == 0)
            {
                return;
            }
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, currentMorph.collisionRadius);
        }
    }
}
