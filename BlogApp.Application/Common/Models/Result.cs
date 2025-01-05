namespace BlogApp.Application.Common.Models
{

    public class Result<T>
    {
        public bool Succeeded { get; private set; }
        public T? Data { get; private set; }  
        public List<string> Errors { get; private set; }

        private Result(bool succeeded, T? data, List<string> errors)
        {
            Succeeded = succeeded;
            Data = data;
            Errors = errors;
        }

        public static Result<T> Success(T data)
            => new Result<T>(true, data, new List<string>());

        public static Result<T> Failure(List<string> errors)
            => new Result<T>(false, default(T), errors); 
    }
}
