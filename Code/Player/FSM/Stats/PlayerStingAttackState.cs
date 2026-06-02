using Member.KJH.Code.Animators;
using Member.KJH.Code.Entities;
using UnityEngine;

namespace Member.KJH.Code.Player.FSM.Stats
{
    public class PlayerStingAttackState : PlayerAttackState
    {
        public PlayerStingAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            _weapon.SetAnime(4);
            MoverComponent.CanManualMove = false;
            MoverComponent.ApplyDashForce(15f);
        }

        public override void Exit()
        {
            MoverComponent.CanManualMove = true;
            _weapon.SetAnime(0);
            base.Exit();
        }
    }
}