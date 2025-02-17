namespace Students.Web.Result
{
    public class Result<T>
    {
        private Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        private Result(T value)
        {
            Value = value;
            IsSuccess = true;
        }

        public static Result<T> Success(T value)
        {
            return new(value);
        }

        public static Result<T> Failure(Error error) => new(false, error);

        public T Value { get; }
    }

    public sealed record Error(string Code, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
