namespace Microondas.API.Wrappers
{
    public class ResponseWrapper<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; } // Changed to nullable reference type
        public bool Success { get; set; }
        public ResponseWrapper(T data, string? message = null, bool success = true) // Changed message to nullable reference type
        {
            Data = data;
            Message = message;
            Success = success;
        }
        public static ResponseWrapper<T> CreateSuccessResponse(T data, string? message = null)
        {
            return new ResponseWrapper<T>(data, message);
        }

        public static ResponseWrapper<T> CreateErrorResponse(string message)
        {
            return new ResponseWrapper<T>(default!, message, false);
        }
    }
}
