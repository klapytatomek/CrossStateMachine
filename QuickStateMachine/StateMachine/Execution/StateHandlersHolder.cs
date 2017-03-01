using System.Collections.Generic;

namespace QuickStateMachine.StateMachine.Execution
{
    internal class StateHandlersHolder
    {
        public StateHandlersHolder()
        {
            Enters = new Dictionary<string, HashSet<StateHandlerInvoker>>();
            Exits = new Dictionary<string, HashSet<StateHandlerInvoker>>();
            Transitions = new Dictionary<KeyValuePair<string, string>, HashSet<StateHandlerInvoker>>();
        }

        public Dictionary<string, HashSet<StateHandlerInvoker>> Enters { get; set; }
        public Dictionary<string, HashSet<StateHandlerInvoker>> Exits { get; set; }
        public Dictionary<KeyValuePair<string, string>, HashSet<StateHandlerInvoker>> Transitions { get; set; }
    }
}