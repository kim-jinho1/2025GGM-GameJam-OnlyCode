using Ami.BroAudio;
using Member.Core;
using UnityEngine;
using Member.KJH.Code.Combat;
using Member.KJH.Code.Entities;
using Member.KJH.Code.Events;
using Member.KJH.Code.Player.FSM;
using Member.KJH.Code.Player.Component;
using Member.KJH.Code.Player.PlayerInput;
using RSJCode.Wave;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Member.KJH.Code.Player
{
    public class Player : Entity, IDamageable
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        [SerializeField] private PlayerStateSO[] stateList;
        
        private PlayerStateMachine _stateMachine;
        public EntityState CurrentState => _stateMachine.CurrentState;

        [SerializeField] private PlayerPosSO _playerPos;
        [SerializeField] private UnityEvent playerHitEvent;

        [field: SerializeField] public SoundID JumpSound { get; private set; } 
        [field:SerializeField] public SoundID AttackSound{ get; private set; }
        [field:SerializeField] public SoundID MoveSound{ get; private set; }
        
        protected override void AfterInitializeComponents()
        {
            base.AfterInitializeComponents();
            _stateMachine = new PlayerStateMachine(this, stateList);
            WaveSystem.Instance.onWaveEnd += HandleWaveEnd;
            Bus<OnUIEvent>.OnEvent += HandlePenaltyUI;
        }

        private void HandlePenaltyUI(OnUIEvent evt)
        {
            PlayerInput.SetInput(evt.IsUI);
        }

        private void HandleWaveEnd()
        {
            PlayerInput.SetInput(false);
        }

        private void OnDestroy()
        {
            WaveSystem.Instance.onWaveEnd -= HandleWaveEnd;
            Bus<OnUIEvent>.OnEvent -= HandlePenaltyUI;
        }

        private void Start()
        {
            if(_playerPos   != null)
                _playerPos.player = gameObject.transform;
            ChangeState(PlayerStates.IDLE);
        }


        private void Update()
        {
            _stateMachine.UpdateMachine();
        }
        
        public void ChangeState(PlayerStates newStateEnum)
        {
            _stateMachine.ChangeState(newStateEnum);
        }

        public void AnimationEndTrigger()
            => _stateMachine.CurrentState.AnimationEndTrigger();

        public void ApplyDamage(int value)
        {
            playerHitEvent.Invoke();
            var stat = GetCompo<PlayerStatComponent>();
            int currentHealth = (int)stat.CurrentHealth;
            currentHealth -= value;
            stat.SetCurrentHealth(currentHealth);
        } 
    }
}