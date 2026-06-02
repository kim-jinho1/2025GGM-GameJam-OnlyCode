using UnityEngine;
using Member.KJH.Code.Player.Component;
using UnityEngine.Events;

namespace Member.KJH.Code.Entities.Component
{
    public class EntityMover : MonoBehaviour, IEntityComponent
    {
        [SerializeField] protected float moveSpeed = 5f;
        [SerializeField] protected float jumpPower = 12f;
        [SerializeField] protected float fallMultiplier = 1.5f;

        [Header("Ground Check")]
        [SerializeField] protected LayerMask whatIsGround;
        [SerializeField] protected Vector2 groundCheckSize;
        [SerializeField] protected Vector2 groundCheckOffset;

        public bool IsGrounded { get; protected set; }

        [field: SerializeField] public Rigidbody2D RbCompo { get; protected set; }

        protected Player.Player _entity;
        protected PlayerStatComponent _playerStatComponent;
        protected float _moveX;

        public int jumpCount = 2;
        protected int _currentJumpCount;

        protected float _moveSpeedMultiplier = 1f;
        protected float _originalGravityScale;

        public bool CanManualMove { get; set; } = true;

        public UnityEvent<bool> OnGroundStatusChange;
        public UnityEvent<float> OnXMoveChange;
        public UnityEvent OnJump;

        private bool _isFalling;
        private readonly float _gravity = -9.8f;

        public void Initialize(IComponentOwner entity)
        {
            _entity = entity as Player.Player;
            _playerStatComponent = _entity.GetCompo<PlayerStatComponent>();
            _originalGravityScale = RbCompo.gravityScale;
            _currentJumpCount = jumpCount;
            
            _playerStatComponent.OnMoveSpeedChanged += HandleSetMoveSpeed;
        }

        private void HandleSetMoveSpeed(int value)
        {
            moveSpeed = value;
        }

        private void OnDestroy()
        {
            _playerStatComponent.OnMoveSpeedChanged -= HandleSetMoveSpeed;
        }


        public void SetMoveSpeedMultiplier(float multiplier)
        {
            _moveSpeedMultiplier = multiplier;
        }

        public void SetGravityScale(float scale)
        {
            RbCompo.gravityScale = _originalGravityScale * scale;
        }

        public bool CanJump()
        {
            return IsGrounded || _currentJumpCount > 0;
        }

        public void Jump()
        {
            if (!CanJump())
                return;

            Vector2 velocity = RbCompo.linearVelocity;
            velocity.y = jumpPower;
            RbCompo.linearVelocity = velocity;

            _currentJumpCount--;
            OnJump?.Invoke();
        }

        public void SetXMove(float xMove)
        {
            _moveX = Mathf.Clamp(xMove, -1f, 1f);
        }

        public void StopImmediately(bool stopY = false)
        {
            RbCompo.linearVelocity = stopY ? Vector2.zero : new Vector2(0f, RbCompo.linearVelocity.y);
            _moveX = 0f;
        }
        
        private void FixedUpdate()
        {
            CheckGround();
            ApplyHorizontalMove();
            ApplyBetterFall();
            CheckFallState();
        }

        protected void CheckGround()
        {
            bool prev = IsGrounded;

            IsGrounded = Physics2D.OverlapBox(
                (Vector2)transform.position + groundCheckOffset,
                groundCheckSize,
                0f,
                whatIsGround
            );

            if (prev != IsGrounded)
            {
                OnGroundStatusChange?.Invoke(IsGrounded);

                if (IsGrounded)
                {
                    _currentJumpCount = jumpCount;
                    SetFalling(false);
                }
            }
        }

        protected void ApplyHorizontalMove()
        {
            if (!CanManualMove)
                return;

            float xVelocity = _moveX * moveSpeed * _moveSpeedMultiplier;
            RbCompo.linearVelocity = new Vector2(xVelocity, RbCompo.linearVelocity.y);

            OnXMoveChange?.Invoke(_moveX);
        }

        protected void ApplyBetterFall()
        {
            if (RbCompo.linearVelocity.y < 0f)
            {
                RbCompo.linearVelocity += Vector2.up * (_gravity * (fallMultiplier - 1f) * Time.fixedDeltaTime);
            }
        }

        protected void CheckFallState()
        {
            bool falling = !IsGrounded && RbCompo.linearVelocity.y < 0f;
            if (_isFalling != falling)
            {
                SetFalling(falling);
            }
        }

        protected void SetFalling(bool value)
        {
            _isFalling = value;
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)transform.position + groundCheckOffset, groundCheckSize);
        }
#endif
    }
}
