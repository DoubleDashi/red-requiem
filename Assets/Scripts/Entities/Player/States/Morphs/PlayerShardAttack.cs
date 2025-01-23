using Entities.Player.Components;
using UnityEngine;

namespace Entities.Player.States.Morphs
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
            
            float distanceBetweenProjectiles = Controller.currentMorph.angle / (Controller.currentMorph.count - 1);
            int middleIndex = Controller.currentMorph.count / 2;

            for (int i = 0; i < Controller.currentMorph.count; i++)
            {
                float angle = (i - middleIndex) * distanceBetweenProjectiles;

                Object.Instantiate(
                    original: Controller.currentMorph.prefab,
                    position: Controller.weaponPivot.position,
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