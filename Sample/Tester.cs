using System.Threading.Tasks;
using QuickStateMachine.Execution;

namespace Sample
{
    public class Tester
    {
        public string Name = "tester";

        public async Task Test()
        {
            await StateMachineExecutor.Current.InitialStateAsync(this, "InitialTestState");
            await StateMachineExecutor.Current.ChangeStateAsync(this, "TestState");
        }
    }
}