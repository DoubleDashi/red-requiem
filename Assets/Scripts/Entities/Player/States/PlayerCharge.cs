using Configs;
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
            if (Controller.components.CurrentMorph is SpearMorph spearMorph)
            {
                spearMorph.ChargeVelocity();
                if (spearMorph.IsCharged() && _usedSFX == false)
                {
                    PlayerEventConfig.OnPlayerChargeComplete?.Invoke(Controller.stats.Guid);
                    _usedSFX = true;
                }
            }
            
            Controller.components.Movement.ForceDecelerate();
            Controller.components.Movement.SetLinearVelocity();
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
    }
}