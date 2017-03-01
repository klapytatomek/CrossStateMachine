using System.Diagnostics;
using QuickStateMachine.StateMachine;
using QuickStateMachine.StateMachine.Attributes;

namespace Sample.States
{
    [StateMachineTransition("InitialTestState", "TestState")]
    internal class TestStateTransitionHandler : IStateHandler<Tester>
    {
        public void Execute(Tester target)
        {
            Debug.WriteLine(target.Name + " transition handled");
        }
    }
}