using System;
using Member.KJH.Code.Animators;
using UnityEngine;

namespace Member.KJH.Code.Entities
{
    public class EntityRenderer : MonoBehaviour, IEntityComponent
    {
        [field: SerializeField] public float FacingDirection { get; private set; } = 1f;

        public event Action OnAnimationEnd;
        public event Action OnMoveSound;
        [SerializeField] private Animator animator;
        
        private IComponentOwner _entity;

        public void Initialize(IComponentOwner entity)
        {
            _entity = entity;
            FacingDirection = 1f;
            _entity.Transform.rotation = Quaternion.Euler(0, 180f, 0);
        }

        public void MoveSound()
        {
            OnMoveSound?.Invoke();
        }
        
        #region Animator parameter section

        public void SetParam(AnimParamSO param, bool value) => animator.SetBool(param.hashValue, value);
        public void SetParam(AnimParamSO param, float value) => animator.SetFloat(param.hashValue, value);
        public void SetParam(AnimParamSO param, int value) => animator.SetInteger(param.hashValue, value);
        public void SetParam(AnimParamSO param) => animator.SetTrigger(param.hashValue);

        #endregion
        
        #region Character Flip Controller Section

        public void FlipController(float xMove)
        {
            if (Mathf.Abs(xMove) < 0.01f)
                return;

            if ((xMove > 0 && FacingDirection < 0) || (xMove < 0 && FacingDirection > 0))
            {
                Flip();
            }
        }

        private void Flip()
        {
            FacingDirection *= -1;
            float targetYAngle = FacingDirection > 0 ? 180f : 0f;
            _entity.Transform.rotation = Quaternion.Euler(0, targetYAngle, 0);
        }

        #endregion

        #region Animation Trigger Section   

        public void AnimationEndTrigger()
        {
            OnAnimationEnd?.Invoke();
        }

        #endregion
    }
}