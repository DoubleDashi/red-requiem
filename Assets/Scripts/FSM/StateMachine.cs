using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public abstract class StateMachine<TStates> : MonoBehaviour where TStates : Enum
    {
        private StateFactory<TStates> _stateFactory;
        private BaseState<TStates> _currentState;
        
        private readonly HashSet<StateTransition<TStates>> _globalTransitions = new();

        protected virtual void OnEnable()
        {
            foreach (BaseState<TStates> state in _stateFactory.GetStates().Values)
            {
                state.Subscribe();
            }
        }
        
        protected virtual void OnDisable()
        {
            foreach (BaseState<TStates> state in _stateFactory.GetStates().Values)
            {
                state.Unsubscribe();
            }
        }

        protected virtual void Update()
        {
            _currentState.Update();
            GlobalTransition();
            Transition();
        }
        
        protected void InitializeStateMachine(StateFactory<TStates> stateFactory, TStates initialState)
        {
            _stateFactory = stateFactory;
            _stateFactory.InitializeStateFactory();
            
            SetGlobalTransitions();
            
            _currentState = _stateFactory.GetState(initialState);
            _currentState.Enter();
        }

        protected void DestroyStateMachine()
        {
            Destroy(gameObject);
        }

        protected virtual void SetGlobalTransitions() { }

        protected void AddGlobalTransition(TStates state, Func<bool> condition)
        {
            _globalTransitions.Add(new StateTransition<TStates>(state, condition));
        }

        private void GlobalTransition()
        {
            foreach (StateTransition<TStates> transition in _globalTransitions)
            {
                if (transition.condition())
                {
                    ChangeState(transition.to);
                    break;
                }
            }
        }

        private void Transition()
        {
            foreach (StateTransition<TStates> transition in _currentState.transitions)
            {
                if (transition.condition())
                {
                    ChangeState(transition.to);
                    break;
                }
            }
        }

        private void ChangeState(TStates state)
        {
            _currentState.Exit();
            _currentState = _stateFactory.GetState(state);
            _currentState.Enter();
        }
        
        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
            if (Application.isPlaying)
            {
                UnityEditor.Handles.Label(transform.position, "Active: " + _currentState.GetType().Name);
            }
            #endif
        }
    }
}