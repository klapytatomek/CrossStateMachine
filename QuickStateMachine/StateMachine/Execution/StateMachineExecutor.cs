using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using QuickStateMachine.StateMachine.Abstraction;
using QuickStateMachine.StateMachine.Attributes;
using QuickStateMachine.StateMachine.Exceptions;

namespace QuickStateMachine.StateMachine.Execution
{
    public class StateMachineExecutor
    {
        private static StateMachineExecutor _current;
        private readonly Dictionary<object, string> _currentStates;
        private Assembly[] _assemblyToLookupIn;
        private Dictionary<Type, StateHandlersHolder> _handlers;
        private bool _isInitialized;

        private StateMachineExecutor()
        {
            var currentdomain =
                typeof(string).GetTypeInfo()
                    .Assembly.GetType("System.AppDomain")
                    .GetRuntimeProperty("CurrentDomain")
                    .GetMethod.Invoke(null, new object[] {});
            var getassemblies = currentdomain.GetType().GetRuntimeMethod("GetAssemblies", new Type[] {});
            var assemblies = getassemblies.Invoke(currentdomain, new object[] {}) as Assembly[];
            var toLookup =
                assemblies.ToList()
                    .Where(
                        t =>
                            !t.FullName.Contains("System") &&
                            !t.FullName.Contains("Microsoft") &&
                            !t.FullName.Contains("mscorlib")).ToArray();
            _assemblyToLookupIn = toLookup;
            _currentStates = new Dictionary<object, string>();
        }

        public static StateMachineExecutor Current => _current ?? (_current = new StateMachineExecutor());

        public void ChangeState<T>(T sender, string state)
        {
            lock (this)
            {
                if (_isInitialized) return;
                _isInitialized = true;
                Initialize();
            }

            if (!_currentStates.ContainsKey(sender))
                throw new StateMachineNotInitializedException();

            if (state.Equals(_currentStates[sender]))
                return;

            var type = sender.GetType();
            if (!_handlers.ContainsKey(type)) return;

            _handlers[type].Execute(_currentStates[sender], state, sender);

            _currentStates[sender] = state;
        }

        public void InitializeWithAssembly(Assembly[] assembly)
        {
            _assemblyToLookupIn = assembly;
            lock (this)
            {
                _isInitialized = true;
                Initialize();
            }
        }

        public void InitialState<T>(T sender, string state)
        {
            if (!_currentStates.ContainsKey(sender))
                _currentStates.Add(sender, state);
            else
                _currentStates[sender] = state;
        }

        private void Initialize()
        {
            _handlers = new Dictionary<Type, StateHandlersHolder>();
            CreateEnterStateHandlers();
            CreateExtiStateHandlers();
            CreateTransitionHandlers();

            if (_handlers.Count == 0)
                throw new StateMachineNoHandlersFoundException();
        }

        private void CreateEnterStateHandlers()
        {
            var enters = _assemblyToLookupIn.SelectMany(x => x.DefinedTypes)
                .Where(x => x.IsDefined(typeof(StateMachineEnterStateAttribute)))
                .Select(x =>
                {
                    var enterStateAttribute = x.GetCustomAttribute<StateMachineEnterStateAttribute>();
                    var type = x.Assembly.GetType(x.FullName);
                    return new
                    {
                        EntersState = enterStateAttribute.State,
                        HandlerFor = enterStateAttribute.Target,
                        Handler = Activator.CreateInstance(type) as IStateHandlerBase
                    };
                }).ToList();

            foreach (var enter in enters)
            {
                if (!_handlers.ContainsKey(enter.HandlerFor))
                    _handlers.Add(enter.HandlerFor, new StateHandlersHolder());

                _handlers[enter.HandlerFor].AddEnter(enter.EntersState, enter.Handler);
            }
        }

        private void CreateExtiStateHandlers()
        {
            var exits = _assemblyToLookupIn.SelectMany(x => x.DefinedTypes)
                .Where(x => x.IsDefined(typeof(StateMachineExitStateAttribute)))
                .Select(x =>
                {
                    var exitStateAttribute = x.GetCustomAttribute<StateMachineExitStateAttribute>();
                    var type = x.Assembly.GetType(x.FullName);
                    return new
                    {
                        ExitState = exitStateAttribute.State,
                        HandlerFor = exitStateAttribute.Target,
                        Handler = Activator.CreateInstance(type) as IStateHandlerBase
                    };
                }).ToList();

            foreach (var exit in exits)
            {
                if (!_handlers.ContainsKey(exit.HandlerFor))
                    _handlers.Add(exit.HandlerFor, new StateHandlersHolder());

                _handlers[exit.HandlerFor].AddExit(exit.ExitState, exit.Handler);
            }
        }

        private void CreateTransitionHandlers()
        {
            var transitions = _assemblyToLookupIn.SelectMany(x => x.DefinedTypes)
                .Where(x => x.IsDefined(typeof(StateMachineTransitionAttribute)))
                .Select(x =>
                {
                    var transitStateAttribute = x.GetCustomAttribute<StateMachineTransitionAttribute>();
                    var type = x.Assembly.GetType(x.FullName);
                    return new
                    {
                        ExitState = transitStateAttribute.FromState,
                        EnterState = transitStateAttribute.ToState,
                        HandlerFor = transitStateAttribute.Target,
                        Handler = Activator.CreateInstance(type) as IStateHandlerBase
                    };
                }).ToList();

            foreach (var transition in transitions)
            {
                var key = new KeyValuePair<string, string>(transition.ExitState, transition.EnterState);
                if (!_handlers.ContainsKey(transition.HandlerFor))
                    _handlers.Add(transition.HandlerFor, new StateHandlersHolder());

                _handlers[transition.HandlerFor].AddTransition(key, transition.Handler);
            }
        }
    }
}