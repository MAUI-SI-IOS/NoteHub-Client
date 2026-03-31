using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace bus.logic.Result;

public static class FunctionnalResult
{
    public static Result<T2, TError> Map<T1, T2, TError>(
        this Result<T1, TError> result, Func<T1, T2> map) =>
        result.IsSuccess
            ? Result<T2, TError>.Success(map(result.Value))
            : Result<T2, TError>.Failure(result.Error);

    public static Result<T, T2Error> MapErr<T, T1Error, T2Error>(
        this Result<T, T1Error> result, Func<T1Error, T2Error> map) =>
        !result.IsSuccess
            ? Result<T, T2Error>.Failure(map(result.Error))
            : Result<T, T2Error>.Success(result.Value);

    public static void Match<T, TError>
        (this Result<T, TError> result, Action<T> ok, Action<TError> err)
    {
        if (result.IsSuccess)
        {
            ok(result.Value);
        }
        else
        {
            err(result.Error);
        }
    }
}

