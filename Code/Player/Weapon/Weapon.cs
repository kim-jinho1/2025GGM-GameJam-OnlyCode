using Member.KJH.Code.Combat;
using UnityEngine;
using Member.KJH.Code.Entities;
using Member.KJH.Code.Player.Component;
using Member.KJH.Code.Player.FSM.Stats;
using System.Collections.Generic;
using RSJCode.Enemy;
using Unity.Cinemachine;

namespace Member.KJH.Code.Player.Weapon
{
    public class Weapon : MonoBehaviour, IEntityComponent
    {
        private PlayerStatComponent _playerStatComponent;
        private Player _player;
        private Animator _animator;

        [SerializeField] private float upKnockbackForce = 8f;
        [SerializeField] private float downKnockbackForce = 12f;
        [SerializeField] private float impulseValue = 0.5f;
        [SerializeField] private CinemachineImpulseSource _source;

        private readonly HashSet<Collider2D> _hitTargets = new();

        private System.Type _lastAttackState;

        public void Initialize(IComponentOwner owner)
        {
            _player = owner as Player;
            _animator = GetComponent<Animator>();

            if (_player != null)
                _playerStatComponent = _player.GetCompo<PlayerStatComponent>();
        }

        private void Update()
        {
            CheckAttackStateChanged();
        }
        
        public void OnAttackStart()
        {
            _hitTargets.Clear();
        }

        private void CheckAttackStateChanged()
        {
            if (_player is null)
                return;

            var currentStateType = _player.CurrentState?.GetType();

            if (currentStateType != _lastAttackState &&
                currentStateType == typeof(PlayerUpAttackState) ||
                currentStateType == typeof(PlayerDownAttackState))
            {
                _hitTargets.Clear();
                _lastAttackState = currentStateType;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TryHit(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TryHit(other);
        }

        private void TryHit(Collider2D other)
        {
            if (!other.CompareTag("Enemy"))
                return;

            if (!IsInAttackState())
                return;

            if (!_hitTargets.Add(other))
                return;

            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                Vector2 dir = Random.insideUnitCircle.normalized;
                _source.GenerateImpulse((Vector3)dir * impulseValue);
                damageable.ApplyDamage(_playerStatComponent.Damage);
            }

            ApplyKnockback(other);
        }

        private void ApplyKnockback(Collider2D other)
        {
            if (!other.TryGetComponent<Rigidbody2D>(out var rigid))
                return;

            rigid.linearVelocity = Vector2.zero;
            
            switch (_player.CurrentState)
            {
                case PlayerDownAttackState:
                    rigid.AddForce(Vector2.up * upKnockbackForce, ForceMode2D.Impulse);
                    break;

                case PlayerUpAttackState:
                    rigid.AddForce(Vector2.down * downKnockbackForce, ForceMode2D.Impulse);
                    break;
                case PlayerStingAttackState:
                    float dir = _player.GetCompo<EntityRenderer>().FacingDirection;
                    Vector2 force = new Vector2(
                        dir * upKnockbackForce,
                        upKnockbackForce
                    );
                    rigid.AddForce(force, ForceMode2D.Impulse);
                    
                    if (other.TryGetComponent<EnemyFrame>(out var enemy))
                    {
                        enemy.ApplyDownSmash(_playerStatComponent.Damage);
                    }

                    break;
            }
        }


        public void AnimeEnd()
        {
            _player.GetCompo<EntityRenderer>().AnimationEndTrigger();
        }
        
        public void SetAnime(int value)
        {
            _animator.SetInteger("Attack", value);
        }

        private bool IsInAttackState()
        {
            if (_player == null)
                return false;

            var currentState = _player.CurrentState;
            return currentState is PlayerUpAttackState or PlayerDownAttackState or PlayerStingAttackState;
        }
    }
}