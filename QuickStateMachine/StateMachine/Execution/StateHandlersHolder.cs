using System.Collections.Generic;
using QuickStateMachine.StateMachine.Abstraction;

namespace QuickStateMachine.StateMachine.Execution
{
    internal class StateHandlersHolder
    {
        public StateHandlersHolder()
        {
            Enters = new Dictionary<string, HashSet<IStateHandlerBase>>();
            Exits = new Dictionary<string, HashSet<IStateHandlerBase>>();
            Transitions = new Dictionary<KeyValuePair<string, string>, HashSet<IStateHandlerBase>>();
        }

        public Dictionary<string, HashSet<IStateHandlerBase>> Enters { get; set; }
        public Dictionary<string, HashSet<IStateHandlerBase>> Exits { get; set; }
        public Dictionary<KeyValuePair<string, string>, HashSet<IStateHandlerBase>> Transitions { get; set; }
    }
}