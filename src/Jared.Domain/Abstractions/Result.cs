﻿namespace Jared.Domain.Abstractions;

public class Result
{
    public bool Success { get; set; }
    public string Error { get; private set; } = default!;
    
    protected Result(bool success, string error)
    {
        if (success && error != string.Empty)
        {
            throw new InvalidOperationException();
        }
        if (!success && error == string.Empty)
        {
            throw new InvalidOperationException();
        }

        Success = success;
        Error = error;
    }

    public static Result Fail(string message)
    {
        return new Result(false, message);
    }
    public static Result<T> Fail<T>(string message)
    {
        return new Result<T>(default(T)!, false, message);
    }
    public static Result Ok()
    {
        return new Result(true, string.Empty);
    }
    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, true, string.Empty);
    }

}

public class Result<T> : Result
{
    public T Data { get; set; }

    protected internal Result(T data, bool success, string error)
        : base(success, error)
    {
        Data = data;
    }
}
