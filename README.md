# QuickStateMachine

QuickStateMachine allows you to handle state machine actions by writing one class per action.

# Instalation

Run following command in Package Manager Console
```c#
Install-Package QuickStateMachine
```

# Configuration

Example class:
```c#
[StateMachineExitState("InitialTestState", typeof(Tester))]
    internal class TestExitStateHandler : StateHandlerBase<Tester>
    {
        public override void Execute(Tester target)
        {
        }
    }
```
It will triggered when state of object of class Tester is changed from InitialTestState to something else.
Object of class Tester will be passed to execute method where you can define your logic for exiting state.

You must derive from class StateHandlerBase<T> where T is class of object it will manage. Not that atribute has also typeof(T) paramater. Attribute and base class type must be the same.

Other attributes are:
```c#
[StateMachineTransition("from", "to", typeof(T))]
```
Triggered when object transitions from state "from" to state "to".

```c#
[StateMachineEnterState("to", typeof(T))]
```
Triggers when object is entering state "to".

# Usage

QuickStateMachine will automaticaly lookup for handlers, but will there is also a chance that it wont find anything. That case call:
```c#
StateMachineExecutor.Current.InitializeWithAssembly(assembly);
```
It takes assemlby in which handlers are defined as parameter.

Next step is to make sure to call
```c#
StateMachineExecutor.Current.InitialState(object, state);
```
for every object that will be handled with state machine.

For changing state call
```c#
StateMachineExecutor.Current.ChangeState(object, state);
```
it will handle all actions defined with Handler classes for given object.

Remember that state is defined for each object, not class.
