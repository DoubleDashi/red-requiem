using System;
using System.Collections.Generic;
using Configs;
using Data;
using Entities.Player.Factories;
using FSM;
using UnityEngine;

namespace Entities.Player
{
    [Serializable]
    public class Morph
    {
        public MorphConfig config;
        public Transform pivotPoint;
        public Transform collisionPoint;
        public LineRenderer lineRenderer;
    }
    
    public class PlayerController : StateMachine<PlayerStateType>
    {
        public PlayerStats stats;

        [HideInInspector] public Camera mainCamera;
        [HideInInspector] public Rigidbody2D body;
        [HideInInspector] public PlayerMorphFactory MorphFactory;
        [HideInInspector] public PlayerMovement Movement;
        
        [SerializeField] private Morph morphSettings;
        [SerializeField] private List<MorphConfig> morphConfigs;
        
        public Morph morph => morphSettings;
        
        //[HideInInspector] public MorphConfig currentMorph;
        //
        // [SerializeField] private Transform weaponCollisionPoint;
        // [SerializeField] private Transform weaponPivotPoint;
        // [SerializeField] private LineRenderer cannonLineRenderer;
        //
        // public Transform weaponCollision => weaponCollisionPoint;
        // public Transform weaponPivot => weaponPivotPoint;
        // public LineRenderer cannonLine => cannonLineRenderer;

        private void Awake()
        {
            mainCamera = Camera.main;
            body = GetComponent<Rigidbody2D>();
            Movement = new PlayerMovement(this);
            MorphFactory = new PlayerMorphFactory(morphConfigs);

            morph.config = MorphFactory.FindByType(MorphType.Spear);
            
            InitializeStateMachine(new PlayerStateFactory(this), PlayerStateType.Idle);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Movement.Rotate();
        }
        
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            if (morph.config == null || morph.config.collisionBox == Vector2.zero)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            float angle = Vector2.SignedAngle(Vector2.right, transform.right);
            Gizmos.matrix = Matrix4x4.TRS(morph.collisionPoint.position, Quaternion.Euler(0, 0, angle), Vector3.one);;
            Gizmos.DrawWireCube(Vector3.zero + (Vector3)morph.config.collisionPointOffset, Vector2.one * morph.config.collisionBox);
        }
    }
}
