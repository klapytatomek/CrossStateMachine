using System;

namespace QuickStateMachine.Attributes
{
    public class StateMachineEnterStateAttribute : Attribute
    {
        public StateMachineEnterStateAttribute(string state, Type target)
        {
            Target = target;
            State = state;
        }

        public string State { get; }
        public Type Target { get; }
    }
}