using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Responses;

public class Result<T>
{
    public bool IsSuccess { get; }
    public List<string> Errors { get; }
    public T Value { get; }

    private Result(T value, bool isSuccess, List<string> errors)
    {
        Value = value;
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result<T> Success(T value) => new Result<T>(value, true, new List<string>());
    public static Result<T> Failure(List<string> errors) => new Result<T>(default, false, errors);
}

