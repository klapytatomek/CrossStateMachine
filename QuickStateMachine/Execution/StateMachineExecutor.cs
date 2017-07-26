using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using QuickStateMachine.Abstraction;
using QuickStateMachine.Attributes;
using QuickStateMachine.Exceptions;

namespace QuickStateMachine.Execution
{
    public class StateMachineExecutor
    {
        private static StateMachineExecutor _current;
        private readonly Dictionary<object, string> _currentStates;
        private Assembly[] _assemblies;
        private Dictionary<Type, StateHandlersHolder> _handlers;
        private bool _isInitialized;
        private readonly SemaphoreSlim _ss = new SemaphoreSlim(1);

        private StateMachineExecutor()
        {
            var currentdomain =
                typeof(string).GetTypeInfo()
                    .Assembly.GetType("System.AppDomain")
                    .GetRuntimeProperty("CurrentDomain")
                    .GetMethod.Invoke(null, new object[] { });
            var getassemblies = currentdomain.GetType().GetRuntimeMethod("GetAssemblies", new Type[] { });
            var assemblies = getassemblies.Invoke(currentdomain, new object[] { }) as Assembly[];
            var toLookup =
                assemblies.ToList()
                    .Where(
                        t =>
                            !t.FullName.Contains("System") &&
                            !t.FullName.Contains("Microsoft") &&
                            !t.FullName.Contains("mscorlib"))
                    .ToArray();
            _assemblies = toLookup;
            _currentStates = new Dictionary<object, string>();
        }

        public static StateMachineExecutor Current => _current ?? (_current = new StateMachineExecutor());

        public async Task ChangeStateAsync<T>(T sender, string state)
        {

            if (!_isInitialized)
                await Initialize();
            
            await _ss.WaitAsync();

            if (!_currentStates.ContainsKey(sender))
                throw new StateMachineNotInitializedException();

            if (state.Equals(_currentStates[sender]))
                return;

            var type = sender.GetType();
            if (!_handlers.ContainsKey(type)) return;

            await _handlers[type].ExecuteAsync(_currentStates[sender], state, sender);

            _currentStates[sender] = state;

            _ss.Release(1);
        }

        public async Task InitializeWithAssembly(Assembly[] assembly)
        {
            _assemblies = assembly;
            await Initialize();
        }

        public async Task InitialStateAsync<T>(T sender, string state)
        {
            await _ss.WaitAsync();

            if (!_currentStates.ContainsKey(sender))
                _currentStates.Add(sender, state);
            else
                _currentStates[sender] = state;

            _ss.Release(1);
        }

        private async Task Initialize()
        {
            await _ss.WaitAsync();

            _handlers = new Dictionary<Type, StateHandlersHolder>();
            CreateEnterStateHandlers();
            CreateExtiStateHandlers();
            CreateTransitionHandlers();

            if (_handlers.Count == 0)
                throw new StateMachineNoHandlersFoundException();

            _isInitialized = true;
            
            _ss.Release(1);
        }

        private void CreateEnterStateHandlers()
        {
            var enters = _assemblies.SelectMany(x => x.DefinedTypes)
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
                })
                .ToList();

            foreach (var enter in enters)
            {
                if (!_handlers.ContainsKey(enter.HandlerFor))
                    _handlers.Add(enter.HandlerFor, new StateHandlersHolder());

                _handlers[enter.HandlerFor].AddEnter(enter.EntersState, enter.Handler);
            }
        }

        private void CreateExtiStateHandlers()
        {
            var exits = _assemblies.SelectMany(x => x.DefinedTypes)
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
                })
                .ToList();

            foreach (var exit in exits)
            {
                if (!_handlers.ContainsKey(exit.HandlerFor))
                    _handlers.Add(exit.HandlerFor, new StateHandlersHolder());

                _handlers[exit.HandlerFor].AddExit(exit.ExitState, exit.Handler);
            }
        }

        private void CreateTransitionHandlers()
        {
            var transitions = _assemblies.SelectMany(x => x.DefinedTypes)
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
                })
                .ToList();

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