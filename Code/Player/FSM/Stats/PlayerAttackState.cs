using Ami.BroAudio;
using Member.KJH.Code.Animators;
using Member.KJH.Code.Entities;

namespace Member.KJH.Code.Player.FSM.Stats
{
    public class PlayerAttackState : PlayerState
    {
        protected Weapon.Weapon _weapon; 
        private new readonly EntityRenderer _renderer;
        
        
        public PlayerAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _renderer = _player.GetCompo<EntityRenderer>();
            _weapon = _player.GetCompo<Weapon.Weapon>();
        }

        public override void Enter()
        {
            base.Enter();
            BroAudio.Play(_player.AttackSound, _player.transform.position);
            _renderer.OnAnimationEnd += HandleAnimationEnd;
        }

        private void HandleAnimationEnd()
        {
            _player.ChangeState(PlayerStates.IDLE);
        }

        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.InputDirection.x;

            MoverComponent.SetXMove(xInput);
        }
        
        public override void Exit()
        {
            _renderer.OnAnimationEnd -= HandleAnimationEnd;
            base.Exit();
        }
    }
}