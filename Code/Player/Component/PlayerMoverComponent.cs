using Ami.BroAudio;
using UnityEngine;
using Member.KJH.Code.Entities;
using Member.KJH.Code.Player.FSM;
using Member.KJH.Code.Entities.Component;

namespace Member.KJH.Code.Player.Component
{
    public class PlayerMoverComponent : EntityMover, IAfterInitializeComponent
    {
        private EntityRenderer _renderer;
        private Player _player;

        public void AfterInitialize()
        {
            _player = _entity as Player;
            _renderer = _entity.GetCompo<EntityRenderer>();
            Debug.Assert(_player != null, $"PlayerMover must attached to player");

            _player.PlayerInput.OnJumpKeyPressed += HandleJumpKeyPress;
            _player.PlayerInput.OnShiftPressed += HandleRunKeyPress;
        }

        private void Update()
        {
            _renderer.FlipController(_moveX);
        }

        private void HandleRunKeyPress(bool value)
        {
            SetMoveSpeedMultiplier(value ? 2f : 1f);
        }

        public void MoveSound()
        {
            BroAudio.Play(_player.MoveSound, _player.transform.position);
        }

        private void HandleJumpKeyPress()
        {
            if (!CanJump())
                return;

            _player.ChangeState(PlayerStates.JUMP);
        }

        public void ApplyDownAttackForce(float force)
        {
            RbCompo.linearVelocity = new Vector2(RbCompo.linearVelocity.x, 0f);
            RbCompo.AddForce(Vector2.down * force, ForceMode2D.Impulse);
        }

        private void OnDestroy()
        {
            _player.PlayerInput.OnJumpKeyPressed -= HandleJumpKeyPress;
            _player.PlayerInput.OnShiftPressed -= HandleRunKeyPress;
        }

        public void ApplyDashForce(float force)
        {
            float dir = _renderer.FacingDirection;
            RbCompo.linearVelocity = new Vector2(0f, RbCompo.linearVelocity.y);
            RbCompo.AddForce(Vector2.right * (dir * force), ForceMode2D.Impulse);
        }
    }
}