using UnityEngine;

namespace Entities.Player.Components
{
    public static class PlayerInput
    {
        public static Vector2 movementDirection => new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        public static Vector2 normalizedMovementDirection => movementDirection.normalized;
        
        public static bool chargeKeyHold => Input.GetKey(KeyCode.Mouse0);
        public static bool chargeKeyPressed => Input.GetKeyDown(KeyCode.Mouse0);
        public static bool chargeKeyReleased => Input.GetKeyUp(KeyCode.Mouse0);
        
        public static bool chargeCancelKeyPressed => Input.GetKeyDown(KeyCode.Mouse1);
    }
}