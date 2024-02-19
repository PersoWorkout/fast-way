namespace Domain.Abstractions
{
    public class Result<T> where T : class?
    {
        private Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }
            IsSucess = isSuccess;
            Error = error;
        }

        private Result(T? data = null)
        {

            IsSucess = true;
            Error = Error.None;
            Data = data;
        }

        public bool IsSucess { get; }

        public bool IsFailure => !IsSucess;

        public Error Error { get; }

        public T? Data { get; }

        public static Result<T> Success(T data) => new(data);
        public static Result<T> Success() => new();

        public static Result<T> Failure(Error error) => new(false, error);
    }
}
