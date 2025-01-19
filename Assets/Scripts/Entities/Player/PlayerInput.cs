using UnityEngine;

namespace Entities.Player
{
    public static class PlayerInput
    {
        public static Vector2 movementDirection => new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        public static Vector2 movementDirectionNormalized => movementDirection.normalized;
        
        public static bool dashPressed => Input.GetKeyDown(KeyCode.Space);
    }
}