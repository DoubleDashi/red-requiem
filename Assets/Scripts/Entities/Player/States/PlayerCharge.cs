using Configs;
using Entities.Player.Morphs;
using Entities.Player.Morphs.Strategies;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerCharge : PlayerState
    {
        private bool _usedSFX;
        private Vector2 _linearVelocity;
        
        public PlayerCharge(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.stats.currentChargeSpeed = 0f;
        }
        
        public override void Update()
        {
            if (Controller.CurrentMorph is SpearMorph spearMorph)
            {
                spearMorph.ChargeVelocity();
                if (spearMorph.IsCharged() && _usedSFX == false)
                {
                    PlayerEventConfig.OnPlayerChargeComplete?.Invoke(Controller.stats.Guid);
                    _usedSFX = true;
                }
            }
            
            Decelerate();
            Controller.body.linearVelocity = _linearVelocity;
        }

        public override void Exit()
        {
            _usedSFX = false;
        }
        
        protected override void SetTransitions()
        {
            AddTransition(PlayerStateType.Idle, () => PlayerInput.chargeCancelKeyPressed);
            AddTransition(PlayerStateType.Attack, () => PlayerInput.chargeKeyReleased);
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