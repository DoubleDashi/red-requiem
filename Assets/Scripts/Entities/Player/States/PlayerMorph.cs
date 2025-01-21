using Entities.Player.Morphs;
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
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Controller.components.CurrentMorph = Controller.components.MorphFactory.GetMorph(MorphType.Spear);
                Debug.Log("Current Morph: " + Controller.components.CurrentMorph.GetType().Name);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Controller.components.CurrentMorph = Controller.components.MorphFactory.GetMorph(MorphType.Shards);
                Debug.Log("Current Morph: " + Controller.components.CurrentMorph.GetType().Name);
            }
            
            Controller.components.Movement.ForceDecelerate();
            Controller.components.Movement.SetLinearVelocity();
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => Input.GetKeyUp(KeyCode.Mouse1));
        }
    }
}