using System.Diagnostics;
using QuickStateMachine.Abstraction;
using QuickStateMachine.Attributes;

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