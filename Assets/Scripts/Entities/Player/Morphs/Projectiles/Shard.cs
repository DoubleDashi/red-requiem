using Controllers;
using UnityEngine;

namespace Entities.Player.Morphs.Projectiles
{
    public class Shard : ProjectileController
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