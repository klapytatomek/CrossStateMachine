using System;

namespace QuickStateMachine.Abstraction
{
    public abstract class StateHandlerBase<T> : IStateHandlerBase
    {
        public abstract void Execute(T target);

        void IStateHandlerBase.AbstractExecute(object target)
        {
            Execute((T)target);
        }
    }
}