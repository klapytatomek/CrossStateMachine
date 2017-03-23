using System.Collections.Generic;
using QuickStateMachine.StateMachine.Abstraction;

namespace QuickStateMachine.StateMachine.Execution
{
    internal class StateHandlersHolder
    {
        private readonly Dictionary<string, HashSet<IStateHandlerBase>> _enters;
        private readonly Dictionary<string, HashSet<IStateHandlerBase>> _exits;
        private readonly Dictionary<KeyValuePair<string, string>, HashSet<IStateHandlerBase>> _transitions;

        public StateHandlersHolder()
        {
            _enters = new Dictionary<string, HashSet<IStateHandlerBase>>();
            _exits = new Dictionary<string, HashSet<IStateHandlerBase>>();
            _transitions = new Dictionary<KeyValuePair<string, string>, HashSet<IStateHandlerBase>>();
        }

        public void Execute(string exit, string enter, object target)
        {
            var key = new KeyValuePair<string, string>(exit, enter);

            var handlersToExecute = new List<IStateHandlerBase>();
            if (_exits.ContainsKey(exit))
                handlersToExecute.AddRange(_exits[exit]);

            if (_transitions.ContainsKey(key))
                handlersToExecute.AddRange(_transitions[key]);

            if (_enters.ContainsKey(enter))
                handlersToExecute.AddRange(_enters[enter]);

            foreach (var stateHandlerAbstractionBase in handlersToExecute)
                stateHandlerAbstractionBase.AbstractExecute(target);
        }

        public void AddExit(string key, IStateHandlerBase handler)
        {
            if (!_exits.ContainsKey(key))
                _exits.Add(key, new HashSet<IStateHandlerBase>());

            _exits[key].Add(handler);
        }

        public void AddTransition(KeyValuePair<string, string> key, IStateHandlerBase handler)
        {
            if (!_transitions.ContainsKey(key))
                _transitions.Add(key, new HashSet<IStateHandlerBase>());

            _transitions[key].Add(handler);
        }

        public void AddEnter(string key, IStateHandlerBase handler)
        {
            if (!_enters.ContainsKey(key))
                _enters.Add(key, new HashSet<IStateHandlerBase>());

            _enters[key].Add(handler);
        }
    }
}