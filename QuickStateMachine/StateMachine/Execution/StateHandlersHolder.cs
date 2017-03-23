using System.Collections.Generic;
using QuickStateMachine.StateMachine.Abstraction;

namespace QuickStateMachine.StateMachine.Execution
{
    internal class StateHandlersHolder
    {
        public StateHandlersHolder()
        {
            Enters = new Dictionary<string, HashSet<IStateHandlerAbstractionBase>>();
            Exits = new Dictionary<string, HashSet<IStateHandlerAbstractionBase>>();
            Transitions = new Dictionary<KeyValuePair<string, string>, HashSet<IStateHandlerAbstractionBase>>();
        }

        public Dictionary<string, HashSet<IStateHandlerAbstractionBase>> Enters { get; set; }
        public Dictionary<string, HashSet<IStateHandlerAbstractionBase>> Exits { get; set; }
        public Dictionary<KeyValuePair<string, string>, HashSet<IStateHandlerAbstractionBase>> Transitions { get; set; }
    }
}