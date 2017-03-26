using QuickStateMachine.Execution;

namespace Sample
{
    public class Tester
    {
        public string Name = "tester";

        public void Test()
        {
            StateMachineExecutor.Current.InitialState(this, "InitialTestState");
            StateMachineExecutor.Current.ChangeState(this, "TestState");
        }
    }
}