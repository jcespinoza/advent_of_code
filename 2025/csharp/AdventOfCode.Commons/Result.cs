namespace AdventOfCode.Commons
{
    // generic. T and E can be a value or reference type
    public record Result<T, E> where T : notnull where E : notnull
    {
        public T Value { get; set; }
        public E Error { get; init; }
        public bool IsSuccess => Error is null;
        public bool IsFailure => !IsSuccess;

        public Result(T value) => Value = value;
        public Result(E error)
        {
            Error = error;
        }

        public static Result<T, E> Success(T value) => new(value);
        public static Result<T, E> Failure(E error) => new(error);
    }
}
