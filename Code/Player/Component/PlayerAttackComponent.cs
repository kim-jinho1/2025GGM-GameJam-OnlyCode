using Member.Core;
using UnityEngine;
using Member.KJH.Code.Entities;
using Member.KJH.Code.Events;
using Member.KJH.Code.Player.FSM;
using Member.KJH.Code.Player.FSM.Stats;

namespace Member.KJH.Code.Player.Component
{
    public class PlayerAttackComponent : MonoBehaviour, IEntityComponent
    {
        private Player _owner;
        private PlayerStatComponent _playerStat;

        private int _attackDamage = 10;

        public void Initialize(IComponentOwner owner)
        {
            _owner = owner as Player;
            _playerStat = _owner?.GetCompo<PlayerStatComponent>();

            if (_playerStat != null)
                _playerStat.OnDamageChanged += HandleDamageChanged;

            _owner.PlayerInput.OnAttackLeftPree += HandleAttackLeftPress;
            _owner.PlayerInput.OnAttackRightPree += HandleAttackRightPress;
            _owner.PlayerInput.OnStingPressed += HandleAttackQPress;
        }

        private void HandleAttackQPress()
        {
            if (IsAttacking())
                return;

            _owner.ChangeState(PlayerStates.STING);
        }

        private void HandleAttackRightPress()
        {
            if (IsAttacking())
                return;

            _owner.ChangeState(PlayerStates.DOWNATTACK);
        }

        private void HandleAttackLeftPress()
        {
            if (IsAttacking())
                return;

            _owner.ChangeState(PlayerStates.UPATTACK);
        }

        private bool IsAttacking()
        {
            return _owner.CurrentState is PlayerDownAttackState or PlayerUpAttackState or PlayerStingAttackState;
        }

        private void OnDestroy()
        {
            if (_playerStat != null)
                _playerStat.OnDamageChanged -= HandleDamageChanged;

            if (_owner != null)
            {
                _owner.PlayerInput.OnAttackLeftPree -= HandleAttackLeftPress;
                _owner.PlayerInput.OnAttackRightPree -= HandleAttackRightPress;
                _owner.PlayerInput.OnStingPressed -= HandleAttackQPress;
            }
        }

        private void HandleDamageChanged(int value)
        {
            _attackDamage = value;
        }
    }
}
