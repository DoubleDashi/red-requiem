using FSM;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerController : StateMachine<PlayerStateType>
    {
        [SerializeField] private EntityStats entityStats;
        [SerializeField] private BoxCollider2D damageHitbox;

        public EntityStats Stats => entityStats;
        public Rigidbody2D Body { get; private set; }
        public Camera MainCamera { get; private set; }
        
        private void Awake()
        {
            MainCamera = Camera.main;
            Body = GetComponent<Rigidbody2D>();

            damageHitbox.enabled = false;
            
            InitializeStateMachine(new PlayerStateFactory(this), PlayerStateType.Idle);
        }

        protected override void Update()
        {
            base.Update();

            Rotate();
        }

        protected override void SetGlobalTransitions()
        {
            AddGlobalTransition(PlayerStateType.Hurt, () => Input.GetKeyDown(KeyCode.Mouse1));
        }

        public void EnableDamageHitbox()
        {
            damageHitbox.enabled = true;
        }
        
        public void DisableDamageHitbox()
        {
            damageHitbox.enabled = false;
        }
        
        private void Rotate()
        {
            Vector2 mousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(0f, 0f, angle);
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Stats.rotationSpeed * Time.deltaTime);
        }
    }
}
