using System.Diagnostics;
using QuickStateMachine.Abstraction;
using QuickStateMachine.Attributes;

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