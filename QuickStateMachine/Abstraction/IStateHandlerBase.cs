namespace QuickStateMachine.Abstraction
{
    internal interface IStateHandlerBase
    {
        void AbstractExecute(object target);
    }
}