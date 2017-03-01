using System;

namespace QuickStateMachine.StateMachine.Exceptions
{
    public class StateMachineNotInitializedException : Exception
    {
        public StateMachineNotInitializedException() : base("State machine was not initialized for object.")
        {
        }
    }
}