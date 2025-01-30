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
                Controller.morph.config = Controller.MorphFactory.FindByType(MorphType.Sword);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Controller.morph.config = Controller.MorphFactory.FindByType(MorphType.Shard);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Controller.morph.config = Controller.MorphFactory.FindByType(MorphType.Hammer);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Controller.morph.config = Controller.MorphFactory.FindByType(MorphType.Cannon);
            }
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => Input.GetKeyUp(KeyCode.Mouse1));
        }
    }
}
