using System;
using UnityEngine;

namespace FSM
{
    public abstract class StateMachine<TStates> : MonoBehaviour where TStates : Enum
    {
        private StateFactory<TStates> _stateFactory;
        private BaseState<TStates> _currentState;
        
        protected virtual void Update()
        {
            _currentState.Update();
            Transition();
        }
        
        protected void InitializeStateMachine(StateFactory<TStates> stateFactory, TStates initialState)
        {
            _stateFactory = stateFactory;
            _stateFactory.InitializeStateFactory();
            
            _currentState = _stateFactory.GetState(initialState);
            _currentState.Enter();
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