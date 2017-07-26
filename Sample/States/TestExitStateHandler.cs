using System.Diagnostics;
using System.Threading.Tasks;
using QuickStateMachine.Abstraction;
using QuickStateMachine.Attributes;

namespace Sample.States
{
    [StateMachineExitState("InitialTestState", typeof(Tester))]
    internal class TestExitStateHandler : StateHandlerBase<Tester>
    {
        public override Task ExecuteAsync(Tester target)
        {
            Debug.WriteLine(target.Name + " exit handled");

            return Task.FromResult(true);
        }
    }
}