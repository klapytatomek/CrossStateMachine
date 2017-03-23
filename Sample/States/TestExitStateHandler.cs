using System.Diagnostics;
using QuickStateMachine.StateMachine.Abstraction;
using QuickStateMachine.StateMachine.Attributes;

namespace Sample.States
{
    [StateMachineExitState("InitialTestState", typeof(Tester))]
    internal class TestExitStateHandler : StateHandlerBase<Tester>
    {
        public override void Execute(Tester target)
        {
            Debug.WriteLine(target.Name + " exit handled");
        }
    }
}