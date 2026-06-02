using Ami.BroAudio;
using UnityEngine;
using Member.KJH.Code.Entities;
using Member.KJH.Code.Animators;

namespace Member.KJH.Code.Player.FSM.Stats
{
    public class PlayerMoveState : PlayerState
    {
        public PlayerMoveState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _renderer.OnMoveSound += HandleMoveSound;
        }

        public override void Exit()
        {
            _renderer.OnMoveSound -= HandleMoveSound;
            base.Exit();
        }

        private void HandleMoveSound()
        {
            BroAudio.Play(_player.MoveSound, _player.transform.position);
        }

        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.InputDirection.x;
            bool isShiftPressed = _player.PlayerInput.IsShiftPressed;

            MoverComponent.SetXMove(xInput);
            if (Mathf.Approximately(xInput, 0))
            {
                _player.ChangeState(PlayerStates.IDLE);
            }
            else if (isShiftPressed)
            {
                _player.ChangeState(PlayerStates.RUN);
            }
        }
    }
}