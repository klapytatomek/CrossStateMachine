using System;

namespace QuickStateMachine.Exceptions
{
    public class StateMachineNotInitializedException : Exception
    {
        public StateMachineNotInitializedException() : base("State machine was not initialized for object.")
        {
        }
    }
}