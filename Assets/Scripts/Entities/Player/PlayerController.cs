using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using Configs.Events;
using Data;
using Entities.Player.Factories;
using FSM;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerController : StateMachine<PlayerStateType>, IEntity
    {
        public PlayerStats stats;
        
        [SerializeField] private Morph morphSettings;
        [SerializeField] private List<MorphConfig> morphConfigs;
        

        [HideInInspector] public Camera mainCamera;
        [HideInInspector] public Rigidbody2D body;
        [HideInInspector] public SpriteRenderer spriteRenderer;
        [HideInInspector] public PlayerMorphFactory MorphFactory;
        [HideInInspector] public PlayerMovement Movement;
        [HideInInspector] public PlayerAnimator Animator;
        [HideInInspector] public bool isHurt;
        
        public Morph morph => morphSettings;
        
        private Coroutine _combatRoutine;

        private void Awake()
        {
            Movement = new PlayerMovement(this);
            MorphFactory = new PlayerMorphFactory(morphConfigs);
            
            mainCamera = Camera.main;
            body = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Animator = new PlayerAnimator(GetComponentInChildren<Animator>());
            morph.config = MorphFactory.FindByType(MorphType.Sword);
            
            InitializeStateMachine(
                new PlayerStateFactory(this), 
                PlayerStateType.Idle
            );
        }

        protected override void Update()
        {
            base.Update();
            Animator.UpdateBlendTree();
            Movement.Flip();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Movement.Rotate();
        }
        
        protected override void SetGlobalTransitions()
        {
            AddGlobalTransition(PlayerStateType.Hurt, () => isHurt);
        }
        
        public void TakeDamage(Damageable damageable)
        {
            isHurt = true;
            
            PlayerEventConfig.OnHurt?.Invoke(stats.guid, damageable);
        }

        public void HandleOnDeath(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void EnableInCombat()
        {
            if (_combatRoutine != null)
            {
                StopCoroutine(_combatRoutine);
                _combatRoutine = null;
            }
            
            _combatRoutine = StartCoroutine(CombatRoutine());
        }

        private void RegenerateBlood()
        {
            if (stats.bloodResource < stats.maxBloodResource)
            {
                stats.bloodResource += stats.bloodResourceRegenSpeed * Time.deltaTime;
            }
        }
        
        private IEnumerator CombatRoutine()
        {
            stats.inCombat = true;
            yield return new WaitForSeconds(stats.outOfCombatDuration);
            stats.inCombat = false;
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
