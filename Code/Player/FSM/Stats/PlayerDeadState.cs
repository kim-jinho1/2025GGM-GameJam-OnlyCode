using Member.Core;
using Member.KJH.Code.Entities;
using Member.KJH.Code.Animators;
using Member.KJH.Code.Events;

namespace Member.KJH.Code.Player.FSM.Stats
{
    public class PlayerDeadState : PlayerState
    {
        public PlayerDeadState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.SetInput(false);
            Bus<PlayerDeadEvent>.Raise(new  PlayerDeadEvent());
        }

        public override void Exit()
        {
            _player.PlayerInput.SetInput(true);
            base.Exit();
        }
    }
}