using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Core
{
    public class ErrorResult<E> : Result<E, E>
    {
        [Pure]
        new public static ErrorResult<E> Err(E value) => new ErrorResult<E>(value);

        private ErrorResult(E error) : base()
        {
            this.State = ResultStatus.Error;
            this.errorResult = error;
        }
    }
}
