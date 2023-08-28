using System.Net;

namespace WebShop.Exceptions
{
    public class UnauthorizedException : BaseCustomException
    {
        public UnauthorizedException(string message) : base(message, null, HttpStatusCode.Unauthorized)
        {
        }
    }
}
