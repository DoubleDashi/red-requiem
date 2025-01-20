using UnityEngine;

namespace Entities.Player
{
    public static class PlayerInput
    {
        public static bool ChargeKeyHold => Input.GetKey(KeyCode.Mouse0);
        public static bool ChargeKeyPressed => Input.GetKeyDown(KeyCode.Mouse0);
        public static bool ChargeKeyReleased => Input.GetKeyUp(KeyCode.Mouse0);
        
        public static bool ChargeCancelKeyPressed => Input.GetKeyDown(KeyCode.Mouse1);
    }
}