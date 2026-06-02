using Member.KJH.Code.Entities;
using Member.KJH.Code.Animators;
using Member.KJH.Code.Player.Component;

namespace Member.KJH.Code.Player.FSM.Stats
{
    public class PlayerState : EntityState
    {
        protected Player _player;
        protected PlayerMoverComponent MoverComponent;

        public PlayerState(Entity entity, AnimParamSO animParam)
            : base(entity, animParam)
        {
            _player = entity as Player;
            MoverComponent = entity.GetCompo<PlayerMoverComponent>();
        }
    }
}