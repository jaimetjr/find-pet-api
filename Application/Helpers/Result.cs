using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class Result<T>
    {
        public bool Success { get; }
        public T? Value { get; }
        public List<string> Errors { get; } = new List<string>();

        public Result(bool success, T? value, List<string> errors)
        {
            Success = success;
            Value = value;
            Errors = errors;
        }

        public static Result<T> Ok(T value) => new(true, value, new());
        public static Result<T> Fail(params string[] errors) => new(false, default, errors.ToList());
        public static Result<T> Fail(IEnumerable<string> errors) => new(false, default, errors.ToList());
    }
}
