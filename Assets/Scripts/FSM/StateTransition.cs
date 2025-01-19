using System;

namespace FSM
{
    public class StateTransition<TStates> where TStates : Enum
    {
        public TStates to { get; private set; }
        public Func<bool> condition { get; private set; }

        public StateTransition(TStates to, Func<bool> condition)
        {
            this.to = to;
            this.condition = condition;
        }

    }
}