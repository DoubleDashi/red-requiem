using System;
using System.Collections.Generic;
using Configs;
using Entities.Player.Components;
using FSM;
using UnityEngine;

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
        [SerializeField] private Transform weaponCollisionPoint;
        [SerializeField] private Transform weaponPivotPoint;
        [SerializeField] private LineRenderer cannonLineRenderer;
        
        [HideInInspector] public MorphConfig currentMorph;

        public Transform weaponCollision => weaponCollisionPoint;
        public Transform weaponPivot => weaponPivotPoint;
        public LineRenderer cannonLine => cannonLineRenderer;
        
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

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            components.Movement.Rotate();
        }
        
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            if (currentMorph == null || currentMorph.collisionBox == Vector2.zero)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            float angle = Vector2.SignedAngle(Vector2.right, transform.right);
            Gizmos.matrix = Matrix4x4.TRS(weaponCollision.position, Quaternion.Euler(0, 0, angle), Vector3.one);;
            Gizmos.DrawWireCube(Vector3.zero + (Vector3)currentMorph.collisionPointOffset, Vector2.one * currentMorph.collisionBox);
        }
    }
}
