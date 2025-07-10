using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Helpers
{
    public class Result<T>
    {
        public bool Success { get; }
        public T? Value { get; }
        public List<string> Errors { get; } = new List<string>();
        public string? Message { get; }

        protected Result(bool success, T? value, List<string> errors, string? message = null)
        {
            Success = success;
            Value = value;
            Errors = errors;
            Message = message;
        }

        public static Result<T> Ok(T value, string? message = null) => new(true, value, new(), message);
        public static Result<T> Fail(params string[] errors) => new(false, default, errors.ToList());
        public static Result<T> Fail(IEnumerable<string> errors) => new(false, default, errors.ToList());
        public static Result<T> Fail(string message, params string[] errors) => new(false, default, errors.ToList(), message);

        public Result<TNew> Map<TNew>(Func<T, TNew> mapper)
        {
            if (!Success || Value == null)
                return Result<TNew>.Fail(Errors.ToArray());

            try
            {
                var newValue = mapper(Value);
                return Result<TNew>.Ok(newValue, Message);
            }
            catch (Exception ex)
            {
                return Result<TNew>.Fail(ex.Message);
            }
        }

        public async Task<Result<TNew>> MapAsync<TNew>(Func<T, Task<TNew>> mapper)
        {
            if (!Success || Value == null)
                return Result<TNew>.Fail(Errors.ToArray());

            try
            {
                var newValue = await mapper(Value);
                return Result<TNew>.Ok(newValue, Message);
            }
            catch (Exception ex)
            {
                return Result<TNew>.Fail(ex.Message);
            }
        }
    }
}
