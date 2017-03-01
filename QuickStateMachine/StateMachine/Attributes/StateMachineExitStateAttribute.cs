using System;

namespace QuickStateMachine.StateMachine.Attributes
{
    public class StateMachineExitStateAttribute : Attribute
    {
        public StateMachineExitStateAttribute(string state)
        {
            State = state;
        }

        public string State { get; }
    }
}