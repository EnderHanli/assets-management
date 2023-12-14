using System.Diagnostics.CodeAnalysis;

namespace Application.Common.Models
{
    public class Result
    {
        protected internal Result(bool succeeded, Error error)
        {
            if (succeeded && error != Error.None || !succeeded && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            Succeeded = succeeded;
            Error = error;
        }

        public bool Succeeded { get; }
        public Error Error { get; }

        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        public static Result<T> Success<T>(T value) => new(value, true, Error.None);
        public static Result<T> Failure<T>(Error error) => new(default!, false, error);
        public static Result<T> Create<T>(T? value) =>
            value is not null ? Success(value) : Failure<T>(Error.NullValue);
    }

    public class Result<T> : Result
    {
        private readonly T? _value;

        protected internal Result(T? value, bool succeeded, Error error)
        : base(succeeded, error)
        {
            _value = value;
        }

        [NotNull]
        public T Value => Succeeded
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can not be accessed");

        public static implicit operator Result<T>(T? value) => Create(value);
    }
}
