using UnityEngine;
using Member.KJH.Code.Entities;

namespace Member.KJH.Code.Player.FSM
{
    public enum PlayerStates
    {
        IDLE, MOVE, DEAD, JUMP, UPATTACK, DOWNATTACK, RUN, UI, STING
    }
    
    [CreateAssetMenu(fileName = "Player state", menuName = "SO/FSM/Player state", order = 0)]
    public class PlayerStateSO : EntityStateSO<PlayerStates> 
    { }
}