using System;

namespace QuickStateMachine.Attributes
{
    public class StateMachineTransitionAttribute : Attribute
    {
        public StateMachineTransitionAttribute(string fromState, string toState, Type target)
        {
            FromState = fromState;
            ToState = toState;
            Target = target;
        }

        public string FromState { get; }
        public string ToState { get; }
        public Type Target { get; }
    }
}