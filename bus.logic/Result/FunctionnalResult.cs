
namespace bus.logic.Result;

public static class FunctionnalResult
{
    //sync
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
    public static TResult Match<T, TError,TResult>
    (this Result<T, TError> result, Func<T, TResult> ok, Func<TError, TResult> err)
    => result.IsSuccess ? ok(result.Value) : err(result.Error);



    //async
    public static async Task<Result<T,E>> MatchAsync<T, E>
    (this Task<Result<T, E>> result, Action<T> ok, Action<E> err)
    {
        var r = await result;

        if (r.IsSuccess)
            ok(r.Value);
        else
            err(r.Error);
        
        return r;
    }
    public static async Task<Result<T, E2>> MapErrAsync<T, E, E2>
    (this Result<T, E> result, Func<E, Task<Result<T, E2>>> mapAsync)
        => result.IsSuccess ? Result<T,E2>.Success(result.Value) : await mapAsync(result.Error);
    public static async Task<Result<T, E2>> MapErrAsync<T, E, E2>
    (this Task<Result<T, E>> result, Func<E, Task<Result<T, E2>>> mapAsync)
        => await (await result).MapErrAsync(mapAsync);

    public static async Task<Result<T, E>> BindErrAsync<T, E>
    (this Result<T, E> result, Func<E, Task<Result<T, E>>> bindAsync)
    => result.IsSuccess ? result : await bindAsync(result.Error);
    
    public static async Task<Result<T, E>> BindErrAsync<T, E>
    (this Task<Result<T, E>> result, Func<E, Task<Result<T, E>>> bindAsync)
    => await (await result).BindErrAsync(bindAsync);

}

