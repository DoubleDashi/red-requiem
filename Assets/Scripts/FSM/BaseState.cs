using System;
using System.Collections.Generic;

namespace FSM
{
    public abstract class BaseState<TStates> where TStates : Enum
    {
        public HashSet<StateTransition<TStates>> transitions { get; private set; } = new();
        
        protected BaseState()
        {
            InitializeBaseState();
        }

        public virtual void Subscribe() { }
        public virtual void Unsubscribe() { }
        
        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }

        protected abstract void SetTransitions();

        protected void AddTransition(TStates state, Func<bool> condition)
        {
            transitions.Add(new StateTransition<TStates>(state, condition));
        }

        private void InitializeBaseState()
        {
            SetTransitions();
        }
    }
}