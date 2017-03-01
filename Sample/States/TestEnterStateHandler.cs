using System.Diagnostics;
using QuickStateMachine.StateMachine;
using QuickStateMachine.StateMachine.Attributes;

namespace Sample.States
{
    [StateMachineEnterState("TestState")]
    public class TestEnterStateHandler : IStateHandler<Tester>
    {
        public void Execute(Tester target)
        {
            Debug.WriteLine(target.Name + " enter handled");
        }
    }
}