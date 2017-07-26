using System.Threading.Tasks;

namespace QuickStateMachine.Abstraction
{
    public abstract class StateHandlerBase<T> : IStateHandlerBase
    {
        async Task IStateHandlerBase.AbstractExecuteAsync(object target)
        {
            await ExecuteAsync((T) target);
        }

        public abstract Task ExecuteAsync(T target);
    }
}