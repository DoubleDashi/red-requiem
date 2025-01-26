using System;
using System.Collections.Generic;
using Entities;
using Entities.Player.Factories;
using UnityEngine;

namespace FSM
{
    public abstract class StateMachine<TStates> : EntityController where TStates : Enum
    {
        private StateFactory<TStates> _stateFactory;
        private BaseState<TStates> _currentState;

        public TStates currentStateType { get; private set; }
        
        private readonly HashSet<StateTransition<TStates>> _globalTransitions = new();

        protected virtual void OnEnable()
        {
            Subscribe();
            
            foreach (BaseState<TStates> state in _stateFactory.GetStates().Values)
            {
                state.Subscribe();
            }
        }
        
        protected virtual void OnDisable()
        {
            Unsubscribe();
            
            foreach (BaseState<TStates> state in _stateFactory.GetStates().Values)
            {
                state.Unsubscribe();
            }
        }
        
        protected virtual void Subscribe() { }
        protected virtual void Unsubscribe() { }

        protected virtual void Update()
        {
            GlobalTransition();
            _currentState.Update();
            Transition();
        }

        protected virtual void FixedUpdate()
        {
            _currentState.FixedUpdate();
        }

        protected void InitializeStateMachine(StateFactory<TStates> stateFactory, TStates initialState)
        {
            _stateFactory = stateFactory;
            _stateFactory.InitializeStateFactory();
            
            SetGlobalTransitions();
            ChangeState(initialState);
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
            _currentState?.Exit();
            _currentState = _stateFactory.GetState(state);
            currentStateType = state;
            _currentState.Enter();
        }
        
        protected virtual void OnDrawGizmos()
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