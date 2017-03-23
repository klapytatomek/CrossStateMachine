using System;

namespace QuickStateMachine.StateMachine.Attributes
{
    public class StateMachineExitStateAttribute : Attribute
    {
        public StateMachineExitStateAttribute(string state, Type target)
        {
            State = state;
            Target = target;
        }

        public string State { get; }
        public Type Target { get; }
    }
}