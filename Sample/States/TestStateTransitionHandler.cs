using System.Diagnostics;
using QuickStateMachine.StateMachine.Abstraction;
using QuickStateMachine.StateMachine.Attributes;

namespace Sample.States
{
    [StateMachineTransition("InitialTestState", "TestState", typeof(Tester))]
    internal class TestStateTransitionHandler : StateHandlerBase<Tester>
    {
        public override void Execute(Tester target)
        {
            Debug.WriteLine(target.Name + " transition handled");
        }
    }
}