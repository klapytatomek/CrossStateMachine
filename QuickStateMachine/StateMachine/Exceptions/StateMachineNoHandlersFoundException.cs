using System;

namespace QuickStateMachine.StateMachine.Exceptions
{
    public class StateMachineNoHandlersFoundException : Exception
    {
        public StateMachineNoHandlersFoundException()
            : base(
                "No handlers found after initialization. If you have any handlers try to call InitializeWithAssembly at application startup."
            )
        {
        }
    }
}