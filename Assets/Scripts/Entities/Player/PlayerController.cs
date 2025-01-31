using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using Configs.Events;
using Data;
using Entities.Player.Factories;
using FSM;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Entities.Player
{
    public class PlayerController : StateMachine<PlayerStateType>, IEntity
    {
        public PlayerStats stats;
        
        [SerializeField] private Morph morphSettings;
        [SerializeField] private List<MorphConfig> morphConfigs;
        public Slider healthSlider;
        public Slider bloodSlider;

        public Material whiteMaterial;
        public Material originalMaterial;
        public Animator anima;
        
        [HideInInspector] public Camera mainCamera;
        [HideInInspector] public Rigidbody2D body;
        [HideInInspector] public SpriteRenderer spriteRenderer;
        [HideInInspector] public PlayerMorphFactory MorphFactory;
        [HideInInspector] public PlayerMovement Movement;
        [HideInInspector] public PlayerAnimator Animator;
        [HideInInspector] public bool isHurt;
        
        public Morph morph => morphSettings;
        
        private Coroutine _combatRoutine;

        public KeyCode morphKey;
        public float originalMaxSpeed;

        private float _maxHealth;
        private float _maxBloodResource;

        protected override void Subscribe()
        {
            PlayerEventConfig.OnDeath += HandleOnDeath;
        }
        
        protected override void Unsubscribe()
        {
            PlayerEventConfig.OnDeath -= HandleOnDeath;
        }
        
        private void Awake()
        {
            Movement = new PlayerMovement(this);
            MorphFactory = new PlayerMorphFactory(morphConfigs);
            
            mainCamera = Camera.main;
            body = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Animator = new PlayerAnimator(GetComponentInChildren<Animator>(), MorphType.Sword);
            morph.config = MorphFactory.FindByType(MorphType.Sword);

            originalMaterial = spriteRenderer.material;

            _maxHealth = stats.health;
            originalMaxSpeed = stats.maxSpeed;
            
            InitializeStateMachine(
                new PlayerStateFactory(this), 
                PlayerStateType.Idle
            );
        }

        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                morphKey = KeyCode.Alpha1;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                morphKey = KeyCode.Alpha2;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                morphKey = KeyCode.Alpha3;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                morphKey = KeyCode.Alpha4;
            }
            
            base.Update();
            Animator.UpdateBlendTree(morph.config.type);
            Movement.Rotate();
            UpdateHealthSlider();
            UpdateBloodSlider();
            RegenerateBlood();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
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
            SceneManager.LoadScene("GameOver"); // ew
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
            if (stats.bloodResource < stats.maxBloodResource && stats.inCombat == false)
            {
                stats.bloodResource += stats.bloodResourceRegenSpeed * Time.deltaTime;
            }
        }
        
        private void UpdateHealthSlider()
        {
            if (healthSlider != null)
            {
                healthSlider.value = Mathf.Clamp01(stats.health / _maxHealth);
            }
        }
        
        private void UpdateBloodSlider()
        {
            if (bloodSlider != null)
            {
                bloodSlider.value = Mathf.Clamp01(stats.bloodResource / stats.maxBloodResource);
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
            
            if (morph.pivotPoint == null)
            {
                return;
            }

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, morph.pivotPoint.position);
            // Save the current Gizmos matrix
            Matrix4x4 oldMatrix = Gizmos.matrix;

            // Set the Gizmos matrix to the desired transformation
            Gizmos.matrix = Matrix4x4.TRS(morph.pivotPoint.position, transform.rotation, Vector3.one);

            // Draw the wire cube with the new transformation
            Gizmos.DrawWireCube(Vector3.zero, morph.config.collisionBox);

            // Restore the original Gizmos matrix
            Gizmos.matrix = oldMatrix;
        }
    }
}
