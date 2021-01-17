using System;
using System.Diagnostics.Contracts;

namespace Core
{
    public class Result<T, E> 
    {
        internal T successResult;
        internal E errorResult;

        public ResultStatus State;

        [Pure]
        public object Case =>
            State switch
            {
                ResultStatus.Success => successResult,
                ResultStatus.Error => errorResult,
                _ => null
            };

       
        [Pure]
        public static Result<T, T> Succ(T value) => SuccessResult<T>.Succ(value);

        [Pure]
        public static Result<E, E> Err(E value) => ErrorResult<E>.Err(value);

        [Pure]
        public Ret Match<Ret>(Func<T, Ret> SuccessFunction, Func<E, Ret> ErrorFunction)
        {
            return State switch
            {
                ResultStatus.Success => SuccessFunction(successResult),
                ResultStatus.Error => ErrorFunction(errorResult),
                _ => throw new InvalidMonadStateException(),
            };
        }

        public void Match(Action<T> SuccessAction, Action<E> ErrorAction)
        {
            switch (State)
            {
                case ResultStatus.Success:
                    SuccessAction(successResult);
                    break;
                case ResultStatus.Error:
                    ErrorAction(errorResult);
                    break;
                default:
                    throw new InvalidMonadStateException();
            }
        }
    }
}
