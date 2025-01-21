using Entities.Player.Components;
using UnityEngine;

namespace Entities.Player.States.Morphs
{
    public class PlayerShardAttack : MorphState
    {
        private bool _isComplete;
        
        private const int ShardCount = 3;
        private const float ConeAngle = 15f;
        
        public PlayerShardAttack(PlayerController controller) : base(controller)
        {
        }
        
        public override void Enter()
        {
            _isComplete = false;
            
            const float distanceBetweenProjectiles = ConeAngle / ShardCount;
            
            for (int i = 0; i < ShardCount; i++)
            {
                float angle = -ConeAngle / 2 + i * distanceBetweenProjectiles;

                Object.Instantiate(
                    original: Controller.currentMorph.prefab,
                    position: Controller.transform.position,
                    rotation: Controller.transform.rotation * Quaternion.Euler(Vector3.forward * angle)
                );
            }

            _isComplete = true;
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => _isComplete && Controller.components.body.linearVelocity == Vector2.zero);
            AddTransition(PlayerStateType.Move, () => _isComplete && Controller.components.body.linearVelocity != Vector2.zero);
        }
    }
}