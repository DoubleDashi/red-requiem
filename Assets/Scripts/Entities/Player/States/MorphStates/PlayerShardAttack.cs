using Entities.Player.Factories;
using UnityEngine;

namespace Entities.Player.States.MorphStates
{
    public class PlayerShardAttack : MorphState
    {
        private bool _isComplete;
        
        public PlayerShardAttack(PlayerController controller) : base(controller)
        {
        }
        
        public override void Enter()
        {
            _isComplete = false;
            
            float distanceBetweenProjectiles = Controller.morph.config.angle / (Controller.morph.config.count - 1);
            int middleIndex = Controller.morph.config.count / 2;

            for (int i = 0; i < Controller.morph.config.count; i++)
            {
                float angle = (i - middleIndex) * distanceBetweenProjectiles;

                Object.Instantiate(
                    original: Controller.morph.config.prefab,
                    position: Controller.morph.pivotPoint.position,
                    rotation: Controller.transform.rotation * Quaternion.Euler(Vector3.forward * angle)
                );
            }

            _isComplete = true;
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => _isComplete && Controller.body.linearVelocity == Vector2.zero);
            AddTransition(PlayerStateType.Move, () => _isComplete && Controller.body.linearVelocity != Vector2.zero);
        }
    }
}