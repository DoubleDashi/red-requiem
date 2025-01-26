using Entities.Player.Factories;
using UnityEngine;

namespace Entities.Player.States.PrimaryStates
{
    public class PlayerMorph : PlayerState
    {
        public PlayerMorph(PlayerController controller) : base(controller)
        {
        }

        public override void Update()
        {
            Controller.Movement.ForceDecelerate();
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Morphing... Shard chosen!");
                Controller.morph.config = Controller.MorphFactory.FindByType(MorphType.Shard);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("Morphing... Sword chosen!");
                Controller.morph.config = Controller.MorphFactory.FindByType(MorphType.Sword);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Morphing... Cannon chosen!");
                Controller.morph.config = Controller.MorphFactory.FindByType(MorphType.Cannon);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("Morphing... Hammer chosen!");
                Controller.morph.config = Controller.MorphFactory.FindByType(MorphType.Hammer);
            }
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => Input.GetKeyUp(KeyCode.Mouse1));
        }
    }
}
