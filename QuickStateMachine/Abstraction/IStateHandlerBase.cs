using System.Threading.Tasks;

namespace QuickStateMachine.Abstraction
{
    internal interface IStateHandlerBase
    {
        Task AbstractExecuteAsync(object target);
    }
}