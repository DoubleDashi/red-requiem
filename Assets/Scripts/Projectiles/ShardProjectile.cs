using Controllers;
using UnityEngine;

namespace Projectiles
{
    public class ShardProjectile : ProjectileController
    {
        protected override void Move()
        {
            transform.position += transform.right * stats.speed * Time.deltaTime;
        }

        protected override void OnOutbound()
        {
            Destroy(gameObject);
        }
    }
}