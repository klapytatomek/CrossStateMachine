namespace QuickStateMachine.StateMachine
{
    public interface IStateHandler<in T>
    {
        void Execute(T target);
    }
}