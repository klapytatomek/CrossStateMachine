using System;

namespace QuickStateMachine.StateMachine.Attributes
{
    public class StateMachineEnterStateAttribute : Attribute
    {
        public StateMachineEnterStateAttribute(string state)
        {
            State = state;
        }

        public string State { get; }
    }
}