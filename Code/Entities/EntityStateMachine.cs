using System;
using System.Collections.Generic;
using UnityEngine;

namespace Member.KJH.Code.Entities
{
    public class EntityStateMachine<TState>
        where TState : Enum
    {
        public delegate void ChangeStateEvent(EntityState oldState, EntityState newState);
        public event ChangeStateEvent OnChangeState;
        
        public EntityState CurrentState { get; private set; }
        public TState CurrentStateEnum { get; private set; }
        private Dictionary<TState, EntityState> _stateDict = new();

        public EntityStateMachine(Entity entity, EntityStateSO<TState>[] stateList)
        {
            foreach (EntityStateSO<TState> state in stateList)
            {
                Type type = Type.GetType(state.className);
                Debug.Assert(type != null, $"State class {state.className} not found.");
                EntityState playerState = Activator.CreateInstance(type, entity, state.animParam) as EntityState;
                Debug.Assert(playerState != null, $"Failed to create instance of {state.className}.");
                
                _stateDict.Add(state.stateName, playerState);
            }
        }
        
        public void ChangeState(TState newStateEnum)
        {
            EntityState newState = _stateDict.GetValueOrDefault(newStateEnum);
            Debug.Assert(newState != null, $"State {newStateEnum} not found in state machine.");

            EntityState oldState = CurrentState;
            
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
            CurrentStateEnum = newStateEnum;
            OnChangeState?.Invoke(oldState, CurrentState);
        }

        public void UpdateMachine()
        {
            CurrentState?.Update();
        }
    }
}