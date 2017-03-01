using System.Reflection;

namespace QuickStateMachine.StateMachine.Execution
{
    internal class StateHandlerInvoker
    {
        public object Handler { get; set; }
        public MethodInfo HandlerMethod { get; set; }

        public void Execute(object target)
        {
            HandlerMethod.Invoke(Handler, new[] { target });
        }
    }
}