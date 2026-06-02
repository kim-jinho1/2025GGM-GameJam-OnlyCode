using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Member.KJH.Code.Entities
{
    public class Entity : MonoBehaviour, IComponentOwner
    {
        protected Dictionary<Type, IEntityComponent> _compoDict = new();
        public Transform Transform => transform;
        
        protected void Awake()
        {
            _compoDict = GetComponentsInChildren<IEntityComponent>(true)
                        .ToDictionary(component=> component.GetType());
            
            InitializeComponents();
            AfterInitializeComponents();
        }
        
        protected virtual void InitializeComponents()
        {
            _compoDict.Values.ToList()
                .ForEach(component => component.Initialize(this));
        }
        
        protected virtual void AfterInitializeComponents()
        {
            _compoDict.Values.OfType<IAfterInitializeComponent>()
                .ToList()
                .ForEach(component => component.AfterInitialize());
        }
        
        public T GetCompo<T>(bool isDerived = false) where T : IEntityComponent
        {
            if (_compoDict.TryGetValue(typeof(T), out IEntityComponent component))
                return (T)component;

            if (!isDerived)
                return default;
            
            Type findType = _compoDict.Keys
                .FirstOrDefault(type => type.IsSubclassOf(typeof(T)) );
            if(findType != null)
                return (T) _compoDict[findType];

            return default;
        }
    }
}