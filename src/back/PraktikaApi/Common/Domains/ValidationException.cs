using System.Net;

namespace CommonShared.Domains
{
    public class ValidationException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }
        public ValidationException(string message, HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
