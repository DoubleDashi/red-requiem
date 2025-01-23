using Configs;
using Controllers;
using UnityEngine;

namespace Projectiles
{
    public class ShardProjectile : ProjectileController
    {
        [SerializeField] private MorphConfig _config;
        
        public MorphConfig config => _config;
        
        protected override void Move()
        {
            transform.position += transform.right * config.speed * Time.deltaTime;
        }

        protected override void OnOutbound()
        {
            ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
            particleSystem.Stop();

            if (particleSystem.IsAlive() == false)
            {
                Destroy(gameObject);    
            }
        }
    }
}