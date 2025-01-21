using UnityEngine;

namespace Entities.Player.Morphs.Strategies
{
    public class ShardMorph : BaseMorph
    {
        private const float ConeAngle = 25f;
        private const int ShardCount = 3;
        
        private bool _isFinished;
        
        public ShardMorph(PlayerController controller, MorphDTO morphDTO) : base(controller, morphDTO)
        {
            HasCharge = false;
        }

        public override void Attack()
        {
            _isFinished = false;
            
            const float distanceBetweenProjectiles = ConeAngle / ShardCount;
            
            for (int i = 0; i < ShardCount; i++)
            {
                float angle = -ConeAngle / 2 + i * distanceBetweenProjectiles;
                
                Object.Instantiate(
                    MorphDTO.morphPrefab,
                    MorphDTO.player.transform.position,
                    MorphDTO.player.transform.rotation * Quaternion.Euler(Vector3.forward * angle)
                );
            }

            _isFinished = true;
        }

        public override bool IsFinished()
        {
            return _isFinished;
        }
    }
}