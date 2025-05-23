﻿using System.Diagnostics.CodeAnalysis;

namespace CleanArchitecture.Domain.Abstractions;

public class Result 
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException("No se puede crear un resultado exitoso con un error.");
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new Result(true, Error.None);
    public static Result Failure(Error error) => new Result(false, error);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
    public static Result<TValue> Create<TValue>(TValue? value) 
        => value is not null
           ? Success(value)
           : Failure<TValue>(Error.NullValue);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;
    [NotNull]
    public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("El resultado del valor de error no es admisible");

    protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
        => _value = value;

    public static implicit operator Result<TValue>(TValue value) => Create(value);
}