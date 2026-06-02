using UnityEngine;
using Member.KJH.Code.Entities;

namespace Member.KJH.Code.Player.Component
{
    public class PlayerJumpComponent : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private new Rigidbody2D rigidbody;
        private float _jumpHeight = 2.5f;

        protected IComponentOwner Entity;

        public void Initialize(IComponentOwner owner)
        {
            Entity = owner;
        }

        public void Jump()
        {
            float jumpForce = Mathf.Sqrt(2f * _jumpHeight * Mathf.Abs(Physics2D.gravity.y * rigidbody.gravityScale));
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpForce);
        }

        public void SetJumpHeight(float height)
        {
            _jumpHeight = height;
        }
    }
}