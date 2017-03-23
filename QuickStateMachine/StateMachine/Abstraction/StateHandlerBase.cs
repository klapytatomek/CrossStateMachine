using System;

namespace QuickStateMachine.StateMachine.Abstraction
{
    public abstract class StateHandlerBase<T> : IStateHandlerAbstractionBase
    {
        public abstract void Execute(T target);

        void IStateHandlerAbstractionBase.AbstractExecute(object target)
        {
            Execute((T)target);
        }
    }
}