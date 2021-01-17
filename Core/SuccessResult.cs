using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Core
{
    public class SuccessResult<T> : Result<T, T>
    {
        [Pure]
        new public static SuccessResult<T> Succ(T value) => new SuccessResult<T>(value);


        private SuccessResult(T value) : base()
        {
            this.State = ResultStatus.Success;
            this.successResult = value;
        }
    }
}
