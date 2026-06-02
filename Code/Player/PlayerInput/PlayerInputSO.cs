using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.KJH.Code.Player.PlayerInput
{
    [CreateAssetMenu(fileName = "Player input", menuName = "SO/Player input", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        public event Action OnJumpKeyPressed;
        public event Action OnAttackRightPree;
        public event Action OnAttackLeftPree;
        public event Action<bool> OnShiftPressed;
        
        public event Action OnStingPressed;
        
        public Vector2 InputDirection { get; private set; }
        
        public bool IsShiftPressed { get; private set; }

        private Controls _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            InputDirection = context.ReadValue<Vector2>();
        }

        public void OnLeftAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnAttackLeftPree?.Invoke();
        }

        public void OnRightAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnAttackRightPree?.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnJumpKeyPressed?.Invoke();
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnShiftPressed?.Invoke(true);
                IsShiftPressed = true;
            }

            if (context.canceled)
            {
                OnShiftPressed?.Invoke(false);
                IsShiftPressed = false;
            }
        }

        public void OnQ(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnStingPressed?.Invoke();
        }

        public void SetInput(bool value)
        {
            if(!value)
                _controls.Player.Disable();
            else
                _controls.Player.Enable();
        }
    }
}