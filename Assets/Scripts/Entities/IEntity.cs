using System;

namespace Entities
{
    public interface IEntity
    {
        public void TakeDamage(Damageable damageable);
        public void HandleOnDeath(Guid guid);
    }
}