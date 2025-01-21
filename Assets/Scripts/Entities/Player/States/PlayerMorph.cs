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

        public override void Enter()
        {
            Controller.components.Movement.Decelerate();
        }

        public override void Update()
        {
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
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => Input.GetKeyUp(KeyCode.Mouse1));
        }
    }
}
