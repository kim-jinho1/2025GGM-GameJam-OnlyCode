using Member.KJH.Code.Entities;
using Member.KJH.Code.Animators;

namespace Member.KJH.Code.Player.FSM.Stats
{
    public class PlayerUpAttackState : PlayerAttackState
    {
        public PlayerUpAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        { }
        
        public override void Enter()
        {
            base.Enter();
            MoverComponent.ApplyDownAttackForce(10f);
            _weapon.SetAnime(2);
        }

        public override void Exit()
        {
            _weapon.SetAnime(0);
            base.Exit();
        }
    }
}