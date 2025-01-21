using Configs.Morphs;
using Entities.Player.Controllers;
using UnityEngine;

namespace Entities.Player.Morphs.Morphs
{
    public class ShardMorph : BaseMorph
    {
        private bool _isComplete;
        
        private const int ShardCount = 4;
        private const float ConeAngle = 20f;
        
        public ShardMorph(PlayerController controller, MorphConfig config) : base(controller, config)
        {
        }

        public override void Enter()
        {
            if (Config is RangedMorphConfig rangedConfig)
            {
                _isComplete = false;
                
                const float distanceBetweenProjectiles = ConeAngle / ShardCount;
                
                for (int i = 0; i < ShardCount; i++)
                {
                    float angle = -ConeAngle / 2 + i * distanceBetweenProjectiles;
                    
                    Object.Instantiate(
                        original: rangedConfig.prefab,
                        position: Controller.transform.position,
                        rotation: Controller.transform.rotation * Quaternion.Euler(Vector3.forward * angle)
                    );
                }
            }

            _isComplete = true;
        }

        public override bool IsComplete()
        {
            return _isComplete;
        }
    }
}