using System;
using Member.KJH.Code.Animators;
using UnityEngine;

namespace Member.KJH.Code.Entities
{
    public abstract class EntityStateSO<TState> : ScriptableObject
        where TState : Enum
    {
        public TState  stateName;
        public string className;
        public AnimParamSO animParam;
    }
}