using UnityEngine;
using Member.KJH.Code.Entities;
using Member.KJH.Code.Animators;

namespace Member.KJH.Code.Player.FSM.Stats
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        { }
        
        public override void Enter()
        {
            base.Enter();
            MoverComponent.StopImmediately();
        }

        public override void Update()
        {
            float xInput = _player.PlayerInput.InputDirection.x;
            bool isShiftPressed = _player.PlayerInput.IsShiftPressed;

            if (Mathf.Abs(xInput) > 0.1f)
            {
                _player.ChangeState(isShiftPressed ? PlayerStates.RUN : PlayerStates.MOVE);
            }
        }
    }
}