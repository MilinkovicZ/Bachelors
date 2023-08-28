using System.Net;

namespace WebShop.Exceptions
{
    public class BadRequestException : BaseCustomException
    {
        public BadRequestException(string message) : base(message, null, HttpStatusCode.BadRequest)
        {
        }
    }
}
