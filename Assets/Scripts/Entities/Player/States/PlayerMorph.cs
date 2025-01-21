using Entities.Player.Morphs;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerMorph : PlayerState
    {
        private Vector2 _linearVelocity;
        
        public PlayerMorph(PlayerController controller) : base(controller)
        {
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Controller.CurrentMorph = Controller.MorphFactory.GetMorph(MorphType.Spear);
                Debug.Log("Current Morph: " + Controller.CurrentMorph.GetType().Name);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Controller.CurrentMorph = Controller.MorphFactory.GetMorph(MorphType.Shards);
                Debug.Log("Current Morph: " + Controller.CurrentMorph.GetType().Name);
            }
            
            Decelerate();
            
            Controller.body.linearVelocity = _linearVelocity;
        }

        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => Input.GetKeyUp(KeyCode.Mouse1));
        }
        
        private void Decelerate()
        {
            if (PlayerInput.movementDirection.x == 0)
            {
                _linearVelocity.x = Mathf.MoveTowards(_linearVelocity.x, 0.0f, Controller.stats.decelerationSpeed * Time.deltaTime);  
            }
            
            if (PlayerInput.movementDirection.y == 0)
            {
                _linearVelocity.y = Mathf.MoveTowards(_linearVelocity.y, 0.0f, Controller.stats.decelerationSpeed * Time.deltaTime); 
            }
        }
    }
}