using Member.Core;

namespace Member.KJH.Code.Events
{
    public struct EnemyAttackEvent : IEvent
    {
        public int Damage { get; }

        public EnemyAttackEvent(int value)
        {
            Damage = value;
        }
    }
}