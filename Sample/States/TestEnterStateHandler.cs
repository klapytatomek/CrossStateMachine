using System.Diagnostics;
using System.Threading.Tasks;
using QuickStateMachine.Abstraction;
using QuickStateMachine.Attributes;

namespace Sample.States
{
    [StateMachineEnterState("TestState", typeof(Tester))]
    public class TestEnterStateHandler : StateHandlerBase<Tester>
    {
        public override Task ExecuteAsync(Tester target)
        {
            Debug.WriteLine(target.Name + " enter handled");
            
            return Task.FromResult(true);
        }
    }
}