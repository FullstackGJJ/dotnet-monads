using System;
using System.Diagnostics.Contracts;

namespace Core
{
    public class Result<T, E>
    {
        internal T successResult;
        internal E errorResult;

        public ResultStatus Status { get; private set; }

        private Result() { }

        private Result(T successValue) 
        {
            Status = ResultStatus.Success;
            successResult = successValue;
        }

        private Result(E errorValue)
        {
            Status = ResultStatus.Error;
            errorResult = errorValue;
        }

        [Pure]
        public object UnsafeResult =>
            Status switch
            {
                ResultStatus.Success => successResult,
                ResultStatus.Error => errorResult,
                _ => null
            };

       
        [Pure]
        public static Result<T, E> Succ(T value) => new Result<T, E>(value);

        [Pure]
        public static Result<T, E> Err(E error) => new Result<T, E>(error);

        [Pure]
        public Ret Match<Ret>(Func<T, Ret> SuccessFunction, Func<E, Ret> ErrorFunction)
        {
            return Status switch
            {
                ResultStatus.Success => SuccessFunction(successResult),
                ResultStatus.Error => ErrorFunction(errorResult),
                _ => throw new InvalidMonadStateException(),
            };
        }

        public void Match(Action<T> SuccessAction, Action<E> ErrorAction)
        {
            switch (Status)
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
