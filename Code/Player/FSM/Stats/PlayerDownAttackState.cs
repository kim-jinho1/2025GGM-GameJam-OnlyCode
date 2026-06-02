using Member.KJH.Code.Entities;
using Member.KJH.Code.Animators;
using Member.KJH.Code.Player.Component;

namespace Member.KJH.Code.Player.FSM.Stats
{
    public class PlayerDownAttackState : PlayerAttackState
    {
        private PlayerJumpComponent _jumpComponent;

        public PlayerDownAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _jumpComponent = entity.GetCompo<PlayerJumpComponent>();
        }

        public override void Enter()
        {
            base.Enter();
            _weapon.SetAnime(1);
        }

        public override void Exit()
        {
            _weapon.SetAnime(0);
            base.Exit();
        }
    }
}