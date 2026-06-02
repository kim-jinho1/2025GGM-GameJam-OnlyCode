using Member.KJH.Code.Animators;

namespace Member.KJH.Code.Entities
{
    public class EntityState
    {
        protected Entity _entity;
        protected AnimParamSO _animParam;
        protected EntityRenderer _renderer;
        protected bool _isTriggerCall;
        
        
        public EntityState(Entity entity, AnimParamSO animParam)
        {
            _entity = entity;
            _animParam = animParam;
            _renderer = _entity.GetCompo<EntityRenderer>(true);
        }
        
        public virtual void Enter()
        {
            _renderer.SetParam(_animParam, true);
            _isTriggerCall = false;
        }
        
        public virtual void Update() {}

        public virtual void Exit()
        {
            _renderer.SetParam(_animParam, false);
        }

        public virtual void AnimationEndTrigger() => _isTriggerCall = true;
    }
}