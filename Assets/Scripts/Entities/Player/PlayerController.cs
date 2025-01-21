using Entities.Player.Morphs;
using FSM;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Player
{
    public class PlayerController : StateMachine<PlayerStateType>
    {
        [SerializeField] private EntityStats entityStats;
        [SerializeField] private BoxCollider2D damageHitbox;

        public MorphDTO morphDTO;
        
        public EntityStats stats => entityStats;
        public Rigidbody2D body { get; private set; }
        public Camera mainCamera { get; private set; }

        public MorphFactory MorphFactory;
        public BaseMorph CurrentMorph;
        
        private void Awake()
        {
            mainCamera = Camera.main;
            body = GetComponent<Rigidbody2D>();

            damageHitbox.enabled = false;
            MorphFactory = new MorphFactory(this, morphDTO);
            CurrentMorph = MorphFactory.GetMorph(MorphType.Shards);
            
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
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(0f, 0f, angle);
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, stats.rotationSpeed * Time.deltaTime);
        }
    }
}
