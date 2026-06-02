using System;
using UnityEngine;
using Member.KJH.Code.Entities;
using Member.KJH.Code.Player.FSM;

namespace Member.KJH.Code.Player.Component
{
    public class PlayerStatComponent : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth = 100;
        [SerializeField] private int damage = 10;
        [SerializeField] private int moveSpeed = 5;

        private const string SETMAXHP_PATH = "MAX_HP";
        private const string SETHP_PATH = "HP";
        private const string SETATTACK_PATH = "ATTACK";
        private const string SETSPEED_PATH = "SPEED";

        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        public int Damage => damage;
        public int MoveSpeed => moveSpeed;

        public event Action<int> OnMaxHealthChanged;
        public event Action<int> OnCurrentHealthChanged;
        public event Action<int> OnDamageChanged;
        public event Action<int> OnMoveSpeedChanged;

        protected Player Entity;

        public void Initialize(IComponentOwner owner)
        {
            Entity = owner as Player;

            SetMaxHealth(PlayerPrefs.GetInt(SETMAXHP_PATH, maxHealth));
            SetCurrentHealth(PlayerPrefs.GetInt(SETHP_PATH, currentHealth));
            SetDamage(PlayerPrefs.GetInt(SETATTACK_PATH, damage));
            SetMoveSpeed(PlayerPrefs.GetInt(SETSPEED_PATH, moveSpeed));
        }

        private void Update()
        {
            if (CurrentHealth <= 0)
                Entity.ChangeState(PlayerStates.DEAD);
        }

        public void SetMaxHealth(int value)
        {
            maxHealth = value;

            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            OnMaxHealthChanged?.Invoke(maxHealth);
        }

        public void SetCurrentHealth(int value)
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            OnCurrentHealthChanged?.Invoke(currentHealth);
        }

        public void SetDamage(int value)
        {
            damage = value;
            OnDamageChanged?.Invoke(damage);
        }

        public void SetMoveSpeed(int value)
        {
            moveSpeed = value;
            OnMoveSpeedChanged?.Invoke(moveSpeed);
        }

        public void TakeDamage(int amount)
        {
            SetCurrentHealth(currentHealth - amount);
        }

        public void Heal(int amount)
        {
            SetCurrentHealth(currentHealth + amount);
        }
    }
}
