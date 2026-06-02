using Ami.BroAudio;
using Member.KJH.Code.Entities;
using Member.KJH.Code.Animators;

namespace Member.KJH.Code.Player.FSM.Stats
{
    public class PlayerJumpState : PlayerState
    {
        private Weapon.Weapon _weapon; 
        
        public PlayerJumpState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _weapon = entity.GetCompo<Weapon.Weapon>();
        }

        public override void Enter()
        {
            base.Enter();
            _weapon.SetAnime(3);
            BroAudio.Play(_player.JumpSound, _player.transform.position);
            MoverComponent.Jump();
            MoverComponent.OnGroundStatusChange.AddListener(HandleGroundStatusChange);
        }

        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.InputDirection.x;

            MoverComponent.SetXMove(xInput);
        }

        public override void Exit()
        {
            _weapon.SetAnime(0);
            MoverComponent.OnGroundStatusChange.RemoveListener(HandleGroundStatusChange);
            base.Exit();
        }

        private void HandleGroundStatusChange(bool value)
        {
            if (value)
            {
                _player.ChangeState(PlayerStates.IDLE);
            }
        }
    }
}