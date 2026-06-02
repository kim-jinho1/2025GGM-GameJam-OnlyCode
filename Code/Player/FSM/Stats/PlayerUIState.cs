using Member.KJH.Code.Animators;
using Member.KJH.Code.Entities;

namespace Member.KJH.Code.Player.FSM.Stats
{
    public class PlayerUIState : PlayerState
    {
        public PlayerUIState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.SetInput(false);
        }

        public override void Exit()
        {
            _player.PlayerInput.SetInput(true);
            base.Exit();
        }
    }
}