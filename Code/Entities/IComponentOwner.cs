using UnityEngine;

namespace Member.KJH.Code.Entities
{
    public interface IComponentOwner
    {
        Transform Transform { get; }

        public T GetCompo<T>(bool isDerived = false) where T : IEntityComponent;
    }
}