using Member.KJH.Code.Entities;
using Member.KJH.Code.Animators;
using UnityEngine;

namespace Member.KJH.Code.Player.FSM.Stats
{
    public class PlayerRunState : PlayerState
    {
        public PlayerRunState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }
        
        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.InputDirection.x;
            
            MoverComponent.SetXMove(xInput);
            if (Mathf.Approximately(xInput, 0))
            {
                _player.ChangeState(PlayerStates.IDLE);
            }
        }
    }
}