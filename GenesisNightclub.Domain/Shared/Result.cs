using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Domain.Shared
{
    public class Result
    {
        public bool IsSuccess { get; private set; }

        public Result(bool isSuccess) { IsSuccess = isSuccess; }

        public static Result Success()
        {
            return new Result(true);
        }
    }

    public class Result<T> : Result
    {
        public Result(bool isSuccess) : base(isSuccess)
        {
        }

        public Result(bool isSuccess, T value) : base(isSuccess)
        {
            Value = value;
        }

        public T? Value { get; private set; }
    }
}
