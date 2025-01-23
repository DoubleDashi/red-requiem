using Entities.Player.Components;
using Entities.Player.Controllers;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerMorph : PlayerState
    {
        public PlayerMorph(PlayerController controller) : base(controller)
        {
        }

        public override void Update()
        {
            Controller.components.Movement.ForceDecelerate();
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Morphing... Spear chosen!");
                Controller.currentMorph = Controller.components.MorphFactory.FindByType(MorphType.Spear);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("Morphing... Shard chosen!");
                Controller.currentMorph = Controller.components.MorphFactory.FindByType(MorphType.Shard);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Morphing... Sword chosen!");
                Controller.currentMorph = Controller.components.MorphFactory.FindByType(MorphType.Sword);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("Morphing... Scythe chosen!");
                Controller.currentMorph = Controller.components.MorphFactory.FindByType(MorphType.Scythe);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Debug.Log("Morphing... Cannon chosen!");
                Controller.currentMorph = Controller.components.MorphFactory.FindByType(MorphType.Cannon);
            }
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => Input.GetKeyUp(KeyCode.Mouse1));
        }
    }
}
