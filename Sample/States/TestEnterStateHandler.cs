using System.Diagnostics;
using QuickStateMachine.StateMachine.Abstraction;
using QuickStateMachine.StateMachine.Attributes;

namespace Sample.States
{
    [StateMachineEnterState("TestState", typeof(Tester))]
    public class TestEnterStateHandler : StateHandlerBase<Tester>
    {
        public override void Execute(Tester target)
        {
            Debug.WriteLine(target.Name + " enter handled");
        }
    }
}