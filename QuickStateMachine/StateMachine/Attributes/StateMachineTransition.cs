using System;

namespace QuickStateMachine.StateMachine.Attributes
{
    public class StateMachineTransitionAttribute : Attribute
    {
        public StateMachineTransitionAttribute(string fromState, string toState)
        {
            FromState = fromState;
            ToState = toState;
        }

        public string FromState { get; }
        public string ToState { get; }
    }
}