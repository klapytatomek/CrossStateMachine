namespace QuickStateMachine.StateMachine.Abstraction
{
    internal interface IStateHandlerBase
    {
        void AbstractExecute(object target);
    }
}