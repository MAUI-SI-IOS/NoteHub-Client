using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace bus.logic.Result
{
    public class Result<T, TError>
    {
        private T? _value;
        private TError? _error;

        public bool IsSuccess { get; }

        public T Value
        {
            get => IsSuccess ? _value! : throw new InvalidOperationException("cant get a null value");
            private set => _value = value;
        }
        public TError Error
        {
            get => !IsSuccess ? _error! : throw new InvalidOperationException("cant get a null value");
            private set => _error = value;
        }
        private Result(bool isSuccess, T? value, TError? error) =>
            (IsSuccess, _value, _error) = (isSuccess, value, error);
        
        public static Result<T, TError> Success(T value) =>
            new Result<T, TError>(true, value, default);

        public static Result<T, TError> Failure(TError error) =>
            new Result<T, TError>(false, default, error);
    }
}
