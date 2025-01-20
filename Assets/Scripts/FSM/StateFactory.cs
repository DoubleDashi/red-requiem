using System;
using System.Collections.Generic;

namespace FSM
{
    public abstract class StateFactory<TStates> where TStates : Enum
    {
        private readonly Dictionary<TStates, BaseState<TStates>> _states = new();

        public void InitializeStateFactory()
        {
            SetStates();
        }
        
        protected abstract void SetStates();
        
        protected void AddState(TStates state, BaseState<TStates> stateInstance)
        {
            _states.Add(state, stateInstance);
        }
        
        public BaseState<TStates> GetState(TStates state)
        {
            return _states[state];
        }
        
        public Dictionary<TStates, BaseState<TStates>> GetStates()
        {
            return _states;
        }
    }
}