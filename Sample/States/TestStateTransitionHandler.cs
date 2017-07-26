using System.Diagnostics;
using System.Threading.Tasks;
using QuickStateMachine.Abstraction;
using QuickStateMachine.Attributes;

namespace Sample.States
{
    [StateMachineTransition("InitialTestState", "TestState", typeof(Tester))]
    internal class TestStateTransitionHandler : StateHandlerBase<Tester>
    {
        public override Task ExecuteAsync(Tester target)
        {
            Debug.WriteLine(target.Name + " transition handled");

            return Task.FromResult(true);
        }
    }
}