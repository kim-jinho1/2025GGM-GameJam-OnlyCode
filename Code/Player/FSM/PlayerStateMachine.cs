using Member.KJH.Code.Entities;

namespace Member.KJH.Code.Player.FSM
{
    public class PlayerStateMachine : EntityStateMachine<PlayerStates>
    {
        public PlayerStateMachine(Entity entity, EntityStateSO<PlayerStates>[] stateList)
            : base(entity, stateList)
        { }
    }
}