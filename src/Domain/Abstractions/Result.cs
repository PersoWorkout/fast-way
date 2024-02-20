namespace Domain.Abstractions
{
    public class Result<T> where T : class?
    {
        private Result(Error error)
        {
            IsSucess = false;
            Errors = [error];
        }

        private Result(List<Error> errors)
        {
            IsSucess = false;
            Errors = errors;
        }

        private Result(T? data = null)
        {

            IsSucess = true;
            Errors = [Error.None];
            Data = data;
        }

        public bool IsSucess { get; }

        public bool IsFailure => !IsSucess;

        public List<Error> Errors { get; }

        public T? Data { get; }

        public static Result<T> Success(T data) => new(data);
        public static Result<T> Success() => new();

        public static Result<T> Failure(Error error) => new(error);
        public static Result<T> Failure(List<Error> errors) => new(errors);
    }
}
