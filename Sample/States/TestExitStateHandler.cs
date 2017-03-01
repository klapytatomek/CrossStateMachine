using System.Diagnostics;
using QuickStateMachine.StateMachine;
using QuickStateMachine.StateMachine.Attributes;

namespace Sample.States
{
    [StateMachineExitState("InitialTestState")]
    internal class TestExitStateHandler : IStateHandler<Tester>
    {
        public void Execute(Tester target)
        {
            Debug.WriteLine(target.Name + " exit handled");
        }
    }
}

