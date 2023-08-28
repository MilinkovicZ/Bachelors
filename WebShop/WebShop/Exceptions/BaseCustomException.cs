using System.Net;

namespace WebShop.Exceptions
{
    public class BaseCustomException : Exception
    {
        public BaseCustomException(string message, List<string>? messages = default, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError) : base(message)
        {
            ErrorMessages = messages;
            ErrorCode = httpStatusCode;
        }
        public List<string>? ErrorMessages { get; set; }
        public HttpStatusCode ErrorCode { get; set; }
    }
}
